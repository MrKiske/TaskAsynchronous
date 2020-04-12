namespace WebApi.Controllers
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("values")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] string q)
        {
            if (string.IsNullOrEmpty(q)) return this.BuildResponse("nothing");

            var response = this.Search(q);

            return this.BuildResponse(response);
        }

        private string Search(string s)
        {
            // dummy implementation - you should use: https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            var client = new WebClient();
            var response = client.DownloadString($"https://www.digi24.ro/cautare?q={s}");
            return response;
        }

        private IActionResult BuildResponse(string data)
        {
            return this.Ok(new {Data = data});
        }
    }
}
