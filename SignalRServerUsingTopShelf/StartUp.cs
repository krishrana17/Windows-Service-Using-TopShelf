using System;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;
using Owin;

namespace SignalRServerUsingTopShelf
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            var hubConfiguration = new HubConfiguration
            {
                EnableDetailedErrors = true
            };

            app.MapSignalR(hubConfiguration);
        }
    }
}
