using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Service.DwhExternalBalances.DataBase;
using Service.DwhExternalBalances.Domain.Models;
using Service.DwhExternalBalances.Grpc;
using Service.DwhExternalBalances.Grpc.Models;

namespace Service.DwhExternalBalances.GrpcServices
{
    public class DwhExternalBalancesesService : IDwhExternalBalancesService
    {
        private readonly IDwhDbContextFactory _dwhDbContextFactory;

        public DwhExternalBalancesesService(IDwhDbContextFactory dwhDbContextFactory)
        {
            _dwhDbContextFactory = dwhDbContextFactory;
        }

        public async Task<GetAllBalancesResponse> GetAllBalancesAsync()
        {
            await using var ctx = _dwhDbContextFactory.Create();
            var data = await ctx.ExternalBalances.ToListAsync();
            return new GetAllBalancesResponse()
            {
                Balances = data
            };
        }
        

        private async Task UpsertBalanceAsync(ExternalBalance externalBalance)
        {
            await using var ctx = _dwhDbContextFactory.Create();
            
            await ctx.ExternalBalances.UpsertRange(externalBalance)
                .On(e => new { e.Name, e.Type, e.Asset })
                .RunAsync();
        }

        public async Task UpsertBankBalanceAsync(ExternalBalance externalBalance)
        {
            var bankBalances = new ExternalBalance()
            {
                Type = "Bank",
                Asset = externalBalance.Asset,
                Volume = externalBalance.Volume
            };

            await UpsertBalanceAsync(bankBalances);
        }
        
        public async Task UpsertFireblocksBalanceAsync(ExternalBalance externalBalance)
        {
            var fireblocks = new ExternalBalance()
            {
                Type = "Fireblocks",
                Volume = externalBalance.Volume,
                Asset = externalBalance.Asset,
                Name = externalBalance.Name
                
            };
            
            await UpsertBalanceAsync(fireblocks);
        }
        
        public async Task UpsertDeFiBalanceAsync(ExternalBalance externalBalance)
        {
            var defi = new ExternalBalance()
            {
                Type = "DeFi",
                Volume = externalBalance.Volume,
                Asset = externalBalance.Asset,
                Name = externalBalance.Name
                
            };
            
            await UpsertBalanceAsync(defi);
        }
        
        public async Task UpsertBinanceFtxBalanceAsync(ExternalBalance externalBalance)
        {
            var binanceFtx = new ExternalBalance()
            {
                Type = externalBalance.Type,
                Volume = externalBalance.Volume,
                Asset = externalBalance.Asset,
                Name = externalBalance.Name
                
            };
            
            await UpsertBalanceAsync(binanceFtx);
        }
    }
}