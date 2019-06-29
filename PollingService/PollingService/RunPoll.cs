using SharedLibrary;
using System.Timers;

namespace PollingService
{
    public class RunPoll
    {
        private AppSettings _appSettings;

        public RunPoll()
        {
            _appSettings = new AppSettings();

        }

        public void Start()
        {
            Timer timer = new Timer();
            timer.Interval = _appSettings.PollingIntervalInMiliSeconds;
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();

        }
        public void Stop()
        {

        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {   
            RabbitMqRepository rabbitMqRepo = new RabbitMqRepository(_appSettings.RabbitMqUsername,
               _appSettings.RabbitMqPassword,
               _appSettings.RabbitMqVirtualHost,
               _appSettings.RabbitMqHost);

            var queueCountDTO = rabbitMqRepo.GetMessageCount(_appSettings.RabbitMqExchange, _appSettings.RabbitMqQueue);
            DbRepository dbRepository = new DbRepository();
            dbRepository.InsertMessageCountIntoQueueHistory(_appSettings.MonitoringDbConnectionString, queueCountDTO);

        }
    }
}
