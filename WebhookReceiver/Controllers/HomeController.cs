using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ConnectWebhookClient.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;

namespace ConnectWebhookClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHubContext<WebSocketHub, IWebSocketHub> _hubContext;

        public HomeController(IHubContext<WebSocketHub, IWebSocketHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost, Route("callback")]
        public async Task<IActionResult> Post([FromBody]JObject model)
        {
            var request = Request;
            await _hubContext.Clients.All.ReceiveMessage(model);

            return Ok();
        }
    }
}
