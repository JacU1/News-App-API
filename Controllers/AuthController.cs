using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using News_App_API.Context;
using News_App_API.Interfaces;
using News_App_API.Models;
using News_App_API.Services;

namespace News_App_API.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly NewsAPIContext _appContext;
        private readonly ITokenInterface _tokenService;
        private readonly IAntiforgery _antiforgery;

        public AuthController(
            NewsAPIContext appContext, 
            ITokenInterface tokenService) 
        {
            _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost, Route("login")]
        [IgnoreAntiforgeryToken]
        public IActionResult Login([FromBody] UserAuthDto loginModel)
        {
            if (loginModel is null)
            {
                return BadRequest(new AuthResponseDto { ErrorMessage = "Invalid client request" });
            }

            var user = _appContext.UsersAuth.FirstOrDefault(u =>
                (u.Email == loginModel.Email) && (u.Password == loginModel.Password));

            if (user is null) {
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            }

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, loginModel.Email),
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;

            TimeZoneInfo polishTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, polishTimeZone);

            user.RefreshTokenExpiryTime = localTime.AddMinutes(60);
            _appContext.SaveChanges();

            return Ok(new AuthResponseDto
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                IsAuthSuccessful = true
            });
        }
    }
}

