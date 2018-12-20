using ConnectWebhookClient.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ConnectWebhookClient
{
    public class WebSocketHub : Hub<IWebSocketHub>
    {
    }

    public interface IWebSocketHub
    {
        Task ReceiveHaloMessage(WebhookPayload model);

        Task ReceiveCamMessage(WebhookPayload model);

        Task ReceiveAuraMessage(WebhookPayload model);

        Task ReceiveClientBaseMessage(WebhookPayload model);
    }
}
