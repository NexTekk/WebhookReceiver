using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace ConnectWebhookClient
{
    public class WebSocketHub : Hub<IWebSocketHub>
    {
    }

    public interface IWebSocketHub
    {
        Task ReceiveMessage(JObject model);
    }
}
