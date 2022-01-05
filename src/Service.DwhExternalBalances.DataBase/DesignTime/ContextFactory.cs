using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Service.DwhExternalBalances.DataBase.DesignTime
{
    public class ContextFactory: IDesignTimeDbContextFactory<DwhContext>
    {
        public ContextFactory()
        {
        }

        public DwhContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DwhContext>();
            optionsBuilder.UseSqlServer();
            //optionsBuilder.UseNpgsql(connString);

            return new DwhContext(optionsBuilder.Options);
        }
    }
}