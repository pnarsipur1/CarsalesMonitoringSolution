using System;
using System.Configuration;

namespace PollingService
{
    class AppSettings
    {
        public int PollingIntervalInMiliSeconds { get { return Convert.ToInt32(ConfigurationManager.AppSettings["PollingIntervalInMiliSeconds"]); } }
        public string RabbitMqUsername { get { return ConfigurationManager.AppSettings["RabbitMqUserName"]; } }
        public string RabbitMqPassword { get { return ConfigurationManager.AppSettings["RabbitMqUserPassword"]; } }
        public string RabbitMqVirtualHost { get { return ConfigurationManager.AppSettings["RabbitMqVirtualHost"]; } }
        public string RabbitMqHost { get { return ConfigurationManager.AppSettings["RabbitMqHost"]; } }

        public string RabbitMqExchange { get { return ConfigurationManager.AppSettings["RabbitMqExchange"]; } }
        public string RabbitMqQueue { get { return ConfigurationManager.AppSettings["RabbitMqQueue"]; } }

        public string MonitoringDbConnectionString { get { return ConfigurationManager.ConnectionStrings["MonitoringDb"].ConnectionString; } }
    }
}
