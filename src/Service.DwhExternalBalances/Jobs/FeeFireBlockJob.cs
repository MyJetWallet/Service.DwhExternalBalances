using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Logging;
using MyJetWallet.Fireblocks.Domain.Models.TransactionHistories;
using MyJetWallet.Sdk.Service.Tools;
using Serilog;
using Service.DwhExternalBalances.DataBase;
using Service.Fireblocks.Api.Client;
using Service.Fireblocks.Api.Grpc;
using Service.Fireblocks.Api.Grpc.Models.SupportedAssets;
using Service.Fireblocks.Api.Grpc.Models.TransactionHistory;

namespace Service.DwhExternalBalances.Jobs
{
    public class FeeFireBlockJob : IStartable
    {
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly ILogger<FeeFireBlockJob> _logger;
        private readonly IDwhDbContextFactory _dwhDbContextFactory;
        private readonly MyTaskTimer _timer;

        public FeeFireBlockJob(ITransactionHistoryService transactionHistoryService,
            ILogger<FeeFireBlockJob> logger,
            IDwhDbContextFactory dwhDbContextFactory 
            )
        {
            _transactionHistoryService = transactionHistoryService;
            _logger = logger;
            _dwhDbContextFactory = dwhDbContextFactory;
            _timer = new MyTaskTimer(nameof(FeeFireBlockJob),TimeSpan.FromSeconds(300), _logger, DoTime);
        }

        private async Task DoTime()
        {
            await GetTransaction();
        }

        private async Task GetTransaction()
        {
            var factory = new FireblocksApiClientFactory(Program.Settings.FireblocksApiUrl);
            var client = factory.GetVaultAccountService();
            var assetsClient = factory.GetSupportedAssetServiceService();
            var transactionHistoryClient = factory.GetTransactionHistoryService();
            var currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var transactionList = new List<TransactionHistory>();
            var transactionHash = new HashSet<string>();

            try
            {
                do
                {
                    var transaction = await transactionHistoryClient.GetTransactionHistoryAsync(
                        new GetTransactionHistoryRequest()
                        {
                            BeforeUnixTime = currentUnixTime,
                            Take = 200
                        });

                    if (transaction.Error != null)
                        break;

                    if (transaction.History == null || !transaction.History.Any())
                        break;

                    var count = 0;
                    foreach (var item in transaction.History)
                    {
                        if (transactionHash.Contains(item.TxHash))
                            continue;

                        if (item.Source != null && (item.Source.Type == TransferPeerPathType.VAULT_ACCOUNT) ||
                            (item.Source.Type == TransferPeerPathType.GAS_STATION))
                        {
                            transactionHash.Add(item.TxHash);
                            transactionList.Add(item);
                            count++;
                        }
                    }

                    if (count == 0)
                        break;

                    currentUnixTime = transaction.History.Last().CreatedDateUnix;
                } while (currentUnixTime != long.MaxValue);


                await using var ctx = _dwhDbContextFactory.Create();
                await ctx.UpsertFeeTransferFireBlocks(transactionList);
                _logger.LogInformation("Fireblock transfer saved {fee}",
                    transactionList.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

        }

        public void Start()
        {
            _timer.Start();
        }
    }
}