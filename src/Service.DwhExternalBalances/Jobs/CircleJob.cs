using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service.Tools;
using MyNoSqlServer.Abstractions;
using Service.Blockchain.Wallets.MyNoSql.Addresses;
using Service.Circle.Signer.Grpc;
using Service.DwhExternalBalances.DataBase;
using Service.DwhExternalBalances.Domain.Models;

namespace Service.DwhExternalBalances.Jobs
{
    public class CircleJob : IStartable
    {
        private readonly IDwhDbContextFactory _dwhDbContextFactory;
        private readonly ICircleBusinessAccountService _circleBusinessAccountService;
        private readonly ILogger<FireBlockJob> _logger;
        private readonly MyTaskTimer _timer;

        public CircleJob(
            IDwhDbContextFactory dwhDbContextFactory,
            ICircleBusinessAccountService circleBusinessAccountService,
            ILogger<FireBlockJob> logger)
        {
            _dwhDbContextFactory = dwhDbContextFactory;
            _circleBusinessAccountService = circleBusinessAccountService;
            _logger = logger;

            _timer = new MyTaskTimer(nameof(FireBlockJob),
                TimeSpan.FromSeconds(180), _logger, DoTime);
        }

        private async Task DoTime()
        {
            await GetCircleBalance();
        }

        private async Task GetCircleBalance()
        {
            try
            {
                var circle = await _circleBusinessAccountService.GetBalances();

                var circleBalances = new List<ExternalBalance>();
                var groupping = circle.Data.Available.GroupBy(x => x.Currency);

                foreach (var group in groupping)
                {
                    var externalBalance = new ExternalBalance()
                    {
                        Asset = group.Key,
                        AssetNetwork = "Circle",
                        Name = "BusinessAccount: " + group.Key,
                        Type = "Circle",
                        Volume = 0m,
                    };

                    foreach (var item in group)
                    {
                        decimal.TryParse(item.Amount, out var res);
                        externalBalance.Volume += res;
                    }

                    circleBalances.Add(externalBalance);
                }

                await using var ctx = _dwhDbContextFactory.Create();
                await using var tr = ctx.Database.BeginTransaction();
                await ctx.Database.ExecuteSqlRawAsync("DELETE FROM data.AllExternalBalances WHERE Type = 'Circle'");

                if (circleBalances.Any())
                    await ctx.UpsertExternalBalances(circleBalances);

                await tr.CommitAsync();
                _logger.LogInformation("Circle saved {balanceCount} balances.",
                    circleBalances.Count);
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