using Microsoft.AspNetCore.Mvc;
using News_App_API.Context;
using News_App_API.Interfaces;
using News_App_API.Models;

namespace News_App_API.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly NewsAPIContext _appContext;
        private readonly ITokenInterface _tokenService;

        public TokenController(NewsAPIContext appContext, ITokenInterface tokenService)
        {
            this._appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
            this._tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        [Route("refresh")]
        public IActionResult Refresh(TokenApiDto tokenApiModel)
        {
            if (tokenApiModel is null) {
                return BadRequest(new AuthResponseDto { ErrorMessage = "Invalid client request" });
            }
             
            string accessToken = tokenApiModel.AccessToken!;
            string refreshToken = tokenApiModel.RefreshToken!;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var userEmail = principal.Identity!.Name;
            var user = _appContext.UsersAuth.SingleOrDefault(u => u.Email == userEmail);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now) {
                return BadRequest(new AuthResponseDto { ErrorMessage = "Invalid client request" });
            }
             
            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            _appContext.SaveChanges();

            return Ok(new AuthResponseDto()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
                IsAuthSuccessful = true
            });
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var userEmail = User.Identity!.Name;
            var user = _appContext.UsersAuth.SingleOrDefault(u => u.Email == userEmail);

            if (user == null) {
                return BadRequest(new AuthResponseDto { ErrorMessage = "Invalid client request" });
            }

            user.RefreshToken = null;
            _appContext.SaveChanges();

            return NoContent();
        }
    }
}

