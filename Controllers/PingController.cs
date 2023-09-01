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

        [HttpGet]
        public IActionResult Get()
        {
            var aft = _antiforgery!.GetAndStoreTokens(HttpContext);
            Response.Cookies.Append("XSRF-TOKEN", aft.RequestToken!, new CookieOptions
            {
                HttpOnly = false,
                Secure = false,
            });

            return Ok(true);
        }
    }
}
