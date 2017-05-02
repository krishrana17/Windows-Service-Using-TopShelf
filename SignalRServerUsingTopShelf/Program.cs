using System;
using System.Linq;
using Topshelf;
using log4net;

namespace SignalRServerUsingTopShelf
{
    class Program
    {
        private static readonly ILog logger = 
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            logger.Info("Programe launched");
            HostFactory.Run(config =>
            {
                config.Service<SignalRServer>(instance =>
                {
                    instance.ConstructUsing(() =>
                        new SignalRServer(logger));

                    instance.WhenStarted(server => server.StartService());

                    instance.WhenStopped(server => server.StopService());
                });

                config.SetServiceName("Signal_server");
                config.SetDisplayName("Signal server");
                config.SetDescription("Self hosted signal server using TopShelf");
                config.StartAutomatically();

                config.BeforeInstall(() =>
                {
                    logger.Info("Service before install");
                });

                config.AfterInstall(() =>
                {
                    logger.Info("Service after install");
                });

            });
            //Console.ReadLine();
        }
    }
}
