using System;
namespace News_App_API.Models
{
	public class AuthResponseDto
	{
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}

