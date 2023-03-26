using System;
using System.Security.Claims;

namespace News_App_API.Interfaces
{
	public interface ITokenInterface
	{
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

