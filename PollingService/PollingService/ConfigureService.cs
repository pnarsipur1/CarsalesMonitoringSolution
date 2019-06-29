using Topshelf;

namespace PollingService
{
    internal static class ConfigureService
    {
        internal static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.Service<RunPoll>(service =>
                {
                    service.ConstructUsing(s => new RunPoll());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });

                //Setup Account that window service use to run.  
                configure.StartAutomatically();
                configure.RunAsLocalSystem();
                configure.SetServiceName("RabbitMqPollingService");
                configure.SetDisplayName("RabbitMqPollingService");
                configure.SetDescription("Polls Message Count from RabbitMq");                
            });
        }
    }
}
