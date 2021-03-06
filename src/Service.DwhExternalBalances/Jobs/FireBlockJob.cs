using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service.Tools;
using MyNoSqlServer.Abstractions;
using Service.Blockchain.Wallets.MyNoSql.Addresses;
using Service.DwhExternalBalances.DataBase;
using Service.DwhExternalBalances.Domain.Models;

namespace Service.DwhExternalBalances.Jobs
{
    public class FireBlockJob: IStartable
    {
        private readonly IMyNoSqlServerDataReader<VaultAssetNoSql> _vaultAssetNoSql;
        private readonly IDwhDbContextFactory _dwhDbContextFactory;
        private readonly ILogger<FireBlockJob> _logger;
        private readonly MyTaskTimer _timer;

        public FireBlockJob(
            IMyNoSqlServerDataReader<VaultAssetNoSql> vaultAssetNoSql, 
            IDwhDbContextFactory dwhDbContextFactory,
            ILogger<FireBlockJob> logger)
        {
            _vaultAssetNoSql = vaultAssetNoSql;
            _dwhDbContextFactory = dwhDbContextFactory;
            _logger = logger;
            
            _timer = new MyTaskTimer(nameof(FireBlockJob),
                TimeSpan.FromSeconds(60), _logger, DoTime);
        }
        
        private async Task DoTime()
        {
            await GetFireblockBalance();
        }

        private async Task GetFireblockBalance()
        {
            try
            {
                var fireblock = _vaultAssetNoSql.Get().ToList();

                var fireblockBalance = new List<ExternalBalance>();
                
                fireblockBalance.AddRange(fireblock.Select(e=>new ExternalBalance()
                {
                    Name = e.VaultAccountId,  
                    Asset = e.AssetSymbol,  
                    Type = "Fireblocks",
                    AssetNetwork = e.AssetNetwork,
                    Volume = e.VaultAsset.Available
                }));

                try
                {
                    await using var ctx = _dwhDbContextFactory.Create();
                    await using var tr = ctx.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
                    await ctx.Database.ExecuteSqlRawAsync(
                        "DELETE FROM data.AllExternalBalances WHERE Type = 'Fireblocks'");
                    await ctx.UpsertExternalBalances(fireblockBalance);
                    await tr.CommitAsync();
                    _logger.LogInformation("Fireblock saved {balanceCount} balances.",
                        fireblockBalance.Count);
                }
                catch (SqlException e)
                {
                    _logger.LogWarning(e, e.Message);
                }
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