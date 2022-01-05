using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Service.DwhExternalBalances.DataBase;
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
    }
}