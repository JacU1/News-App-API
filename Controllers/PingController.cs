using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace News_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly IAntiforgery? _antiforgery;

        public PingController(IAntiforgery? antiforgery)
        {
            _antiforgery = antiforgery;
        }

        [Route("antiforgerytoken")]
        [IgnoreAntiforgeryToken]
        [HttpGet]
        public IActionResult GenerateAntiForgeryTokens()
        {
            var tokens = _antiforgery!.GetAndStoreTokens(HttpContext);
            Response.Cookies.Append("CSRF-COOKIE", tokens.RequestToken!);
            return NoContent();
        }

        [Route("Startupcall")]
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public IActionResult Startupcall()
        {
            return NoContent();
        }
    }
}
