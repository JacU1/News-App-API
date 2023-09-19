using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace News_App_API.Controllers
{
    [AutoValidateAntiforgeryToken]
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
            Response.Cookies.Append("XSRF-COOKIE", tokens.RequestToken!, new CookieOptions
            {
                HttpOnly = false,
                IsEssential = true,
                SameSite = SameSiteMode.Lax,
                Secure = true,
                Path = "/"
            });
            return NoContent();
        }

       /* options.HeaderName = "X-XSRF-TOKEN";
        options.Cookie.Name = "XSRF-COOKIE";
        options.Cookie.HttpOnly = false;
        options.Cookie.Domain = "http://localhost:4200";
        options.Cookie.IsEssential = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.Path = "/";*/

        [Route("Startupcall")]
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public IActionResult Startupcall()
        {
            return NoContent();
        }

        [Route("csrftest")]
        [HttpPost]
        public async Task<IActionResult> csrfTestAsync()
        {
            var test = _antiforgery.ValidateRequestAsync(HttpContext);
            Console.WriteLine(test);

            try
            {
                await _antiforgery.ValidateRequestAsync(HttpContext);
            }
            catch (AntiforgeryValidationException exception)
            {
                Console.WriteLine(exception.Message, exception);
            }

            return NoContent();
        }
    }
}
