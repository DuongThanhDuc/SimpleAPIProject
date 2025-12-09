using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using services;
using utilities;
using models;

namespace controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthControllers : ControllerBase
    {
        private readonly UserServices _userServices;
        private readonly TokenGenerator _tokenGenerator;
        private const string SecretKey = "ThisIsASecretKeyForJWTTokenGeneration";
        
        public AuthControllers( UserServices userServices, TokenGenerator tokenGenerator)
        {
            _userServices = userServices;
            _tokenGenerator = tokenGenerator;            
        }

        [HttpPost("login")]
         public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userServices.LoadUsers().FirstOrDefault(
                u => u.Username == request.Username &&
                u.Password == request.Password
            );

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _tokenGenerator.GenerateToken(user);
            return Ok(new { Token = token });
        }


    }
}