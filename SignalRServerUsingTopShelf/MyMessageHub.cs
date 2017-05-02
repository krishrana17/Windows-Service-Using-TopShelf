using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRServerUsingTopShelf
{
    public class MyMessageHub : Hub
    {
        public static Dictionary<string, string> subscribedClients = new Dictionary<string, string>();

        public void Connect(string connectionId, string clientID)
        {
            if (!subscribedClients.ContainsKey(clientID))
            {
                subscribedClients.Add(clientID, connectionId);
            }
            else
            {
                subscribedClients[clientID] = connectionId;
            }
        }

        public void BroadcastMessage(string message)
        {
            Clients.All.BroadcastMessage(message);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var connectionId = subscribedClients.FirstOrDefault(x => x.Value == Context.ConnectionId);

            if (!string.IsNullOrEmpty(connectionId.Value))
            {
                subscribedClients.Remove(connectionId.Key);
            }
            return base.OnDisconnected(true);
        }
    }
}
