using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ConnectWebhookClient.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ConnectWebhookClient.Controllers
{
    public class HomeController : Controller
    {
        //private readonly WebSocketHub _hub;
        private readonly IHubContext<WebSocketHub, IWebSocketHub> _hubContext;

        public HomeController(IHubContext<WebSocketHub, IWebSocketHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("halo")]
        public async Task<IActionResult> HaloCallback([FromBody]WebhookPayload model)
        {
            var request = Request;
            await _hubContext.Clients.All.ReceiveHaloMessage(model);
            
            return Ok();
        }

        [Route("aura")]
        public async Task<IActionResult> AuraCallback([FromBody]WebhookPayload model)
        {
            await _hubContext.Clients.All.ReceiveAuraMessage(model);

            return Ok();
        }

        [Route("cam")]
        public async Task<IActionResult> CamCallback([FromBody]WebhookPayload model)
        {
            await _hubContext.Clients.All.ReceiveCamMessage(model);

            return Ok();
        }

        [Route("clientbase")]
        public async Task<IActionResult> ClientBaseCallback([FromBody]WebhookPayload model)
        {
            await _hubContext.Clients.All.ReceiveClientBaseMessage(model);

            return Ok();
        }
    }
}
