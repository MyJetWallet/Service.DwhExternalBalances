using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.NoSql;
using MyJetWallet.Sdk.Service;
using Service.DwhExternalBalances.Jobs;

namespace Service.DwhExternalBalances
{
    public class ApplicationLifetimeManager : ApplicationLifetimeManagerBase
    {
        
        private readonly MyNoSqlClientLifeTime _myNoSqlClientLifeTime;
        private readonly ILogger<ApplicationLifetimeManager> _logger;
        private readonly DictionariesJob _dictionariesJob;

        public ApplicationLifetimeManager(
            IHostApplicationLifetime appLifetime, 
            ILogger<ApplicationLifetimeManager> logger,
            MyNoSqlClientLifeTime myNoSqlClientLifeTime, 
            DictionariesJob dictionariesJob)
            : base(appLifetime)
        {
            _logger = logger;
            _myNoSqlClientLifeTime = myNoSqlClientLifeTime;
            _dictionariesJob = dictionariesJob;
        }

        protected override void OnStarted()
        {
            _logger.LogInformation("OnStarted has been called.");
            _myNoSqlClientLifeTime.Start();
            _dictionariesJob.Start();
        }

        protected override void OnStopping()
        {
            _logger.LogInformation("OnStopping has been called.");
            _myNoSqlClientLifeTime.Stop();
        }

        protected override void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called.");
        }

    }
}
