using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebhookReceiver.Controllers
{
    [Route("events")]
    public class EventsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public EventsController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpPost("bitbucket")]
        public async Task<IActionResult> ReceiveBitbucketEvent([FromBody] JObject payload)
        {
            var data = GetPostbackData(payload);
            var url = _configuration["BitbucketPipelineUrl"];
            var httpClient = _httpClientFactory.CreateClient("bitbucket");
            var response = await httpClient.PostAsJsonAsync(url, data);

            response.EnsureSuccessStatusCode();

            return Ok();
        }

        private IDictionary<string, object> GetPostbackData(JObject payload)
        {
            var branchName = payload["pullrequest"]["source"]["branch"]["name"].Value<string>();

            if (string.IsNullOrWhiteSpace(branchName))
            {
                throw new InvalidCastException("Cannot parse payload");
            }

            var pipelineName = _configuration["BitbucketPipelineName"];
            var data = new Dictionary<string, object>
            {
                {
                    "target",
                    new Dictionary<string, object>
                    {
                        { "type", "pipeline_ref_target" },
                        { "ref_name", branchName },
                        { "ref_type", "branch" },
                        {
                            "selector",
                            new Dictionary<string, string>
                            {
                                { "type", "custom" },
                                { "pattern", pipelineName }
                            }
                        }
                    }
                }
            };

            return data;
        }
    }
}
