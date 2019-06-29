using System.Configuration;

namespace MonitoringSite.Helpers
{
    public class AppSettings
    {        
        public string RabbitMqUsername { get { return ConfigurationManager.AppSettings["RabbitMqUserName"]; } }
        public string RabbitMqPassword { get { return ConfigurationManager.AppSettings["RabbitMqUserPassword"]; } }
        public string RabbitMqVirtualHost { get { return ConfigurationManager.AppSettings["RabbitMqVirtualHost"]; } }
        public string RabbitMqHost { get { return ConfigurationManager.AppSettings["RabbitMqHost"]; } }

        public string RabbitMqExchange { get { return ConfigurationManager.AppSettings["RabbitMqExchange"]; } }        

        public string MonitoringDbConnectionString { get { return ConfigurationManager.ConnectionStrings["MonitoringDb"].ConnectionString; } }
    }
}