using System;
using System.Linq;
using Microsoft.Owin.Hosting;
using log4net;

namespace SignalRServerUsingTopShelf
{
    public class SignalRServer
    {
        private ILog logger;

        IDisposable SignalR { get; set; }

        public SignalRServer(ILog logger)
        {
            this.logger = logger;
        }

        public bool StartService()
        {
            logger.Info("Starting service...");
            var option = new StartOptions();
            //option.Urls.Add("http://localhost:18275");
            // You can either get dynamic ip OR set in app.config
            // But for demo purpose i have set it static
            option.Urls.Add("http://192.168.151.87:18275");
            SignalR = WebApp.Start<StartUp>(option);
            logger.Info("SignalR server started..");
            logger.Info("Service Started.");
            return true;
        }

        public bool StopService()
        {
            SignalR.Dispose();
            logger.Info("Service Stopped.");
            System.Threading.Thread.Sleep(1500);
            return true;
        }

        public bool PauseService()
        {
            logger.Info("I'm in Pause method");
            return true;
        }

    }
}
