using System;
using Microsoft.AspNet.SignalR.Client;
using System.Threading;

namespace MessageSender
{
    class Program
    {

        private const string ServerUri = "http://192.168.151.87:18275/signalr";

        static void Main(string[] args)
        {
            //Signalr server takes some time to start, so i have sleep thread for 1 sec
            Thread.Sleep(1000);

            ConnectServer("MessageSender");

            while (true)
            {
                var message = Console.ReadLine();
                if (message == "x")
                    break;

                SendMessage(message);
            }
        }

        private static void SendMessage(string message)
        {
            HubProxy.Invoke("BroadcastMessage", message);
        }

        private static void ConnectServer(string clientId)
        {
            Console.WriteLine($"{clientId} connecting to server..");
            //SignalRConnection = new HubConnection("http://" + ConfigurationManager.AppSettings["ServerUri"] + "/signalr");
            SignalRConnection = new HubConnection(ServerUri);
            HubProxy = SignalRConnection.CreateHubProxy("MyMessageHub");

            try
            {
                SignalRConnection.Start().Wait();

                SignalRConnection.StateChanged += delegate (StateChange state)
                {
                    if (state.NewState == ConnectionState.Connected)
                    {
                        //HubProxy.Invoke("Connect", SignalRConnection.ConnectionId, clientId);
                        Console.WriteLine("Client connected.");
                    }
                };
                Console.WriteLine("Connection Successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static IHubProxy HubProxy { get; set; }

        public static HubConnection SignalRConnection { get; set; }

    }
}
