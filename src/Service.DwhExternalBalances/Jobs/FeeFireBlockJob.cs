﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore;
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
            
            var currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var lastTsFromDatabase = await GetLastTimeFromDb();
            
            //todo: remove after finish all tests
            {
                lastTsFromDatabase = 0;
                _logger.LogWarning("GetTransaction fireblocks reset last time to 0");
            }

            var transactionPrev = new List<(string, string)>();

            {
                await using var ctx = _dwhDbContextFactory.Create();
                await ctx.Database.ExecuteSqlRawAsync($"delete from data.TransactionFireBlocks where UpdatedDateUnix >= {lastTsFromDatabase}");
            }
            
            try
            {
                do
                {
                    var transaction = await _transactionHistoryService.GetTransactionHistoryAsync(
                        new GetTransactionHistoryRequest()
                        {
                            BeforeUnixTime = currentUnixTime,
                            Take = 200
                        });

                    if (transaction.Error != null)
                    {
                        _logger.LogError($"Cannot get transactions. error: {transaction.Error.Message} ({transaction.Error.ErrorCode}). FromTime: {currentUnixTime}");
                        break;
                    }

                    if (transaction.History == null || !transaction.History.Any())
                        break;


                    var listToUpdate = new List<TransactionHistory>();
                    foreach (var item in transaction.History)
                    {
                        if (!transactionPrev.Contains((item.TxHash, item.FireblocksAssetId))
                            && item.UpdatedDateUnix >= lastTsFromDatabase)
                        {
                            listToUpdate.Add(item);
                            transactionPrev.Add((item.TxHash, item.FireblocksAssetId));
                        }
                    }
                    
                    if (!listToUpdate.Any())
                        break;
                    
                    await using var ctx = _dwhDbContextFactory.Create();
                    //await ctx.UpsertFeeTransferFireBlocks(listToUpdate);
                    ctx.TransactionHistories.AddRange(listToUpdate);
                    await ctx.SaveChangesAsync();
                    _logger.LogInformation("Fireblock transaction saved {count}, from {unixTime}", listToUpdate.Count, currentUnixTime);

                    currentUnixTime = transaction.History.Min(e => e.UpdatedDateUnix);
                } while (currentUnixTime > lastTsFromDatabase);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on GetTransaction from fireblocks");
            }

        }

        private async Task<long> GetLastTimeFromDb()
        {
            await using var ctx = _dwhDbContextFactory.Create();
            
            var lastRecord = await ctx
                .TransactionHistories
                .OrderByDescending(e => e.UpdatedDateUnix)
                .FirstOrDefaultAsync();
            
            return lastRecord?.UpdatedDateUnix ?? 0;
        }

        public void Start()
        {
            _timer.Start();
        }
    }
}