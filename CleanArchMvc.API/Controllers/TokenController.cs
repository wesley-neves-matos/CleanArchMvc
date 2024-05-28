using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authentication;
        private readonly IConfiguration _configuration;

        public TokenController(IAuthenticate authenticate, IConfiguration configuration)
        {
            _authentication = authenticate ?? throw new ArgumentNullException(nameof(authenticate));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(authenticate));
        }

        [HttpPost("CreateUser")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> CreateUser(LoginModel userInfo)
        {
            var result = await _authentication.RegisterUserAsync(userInfo.Email, userInfo.Password);

            if (result)
            {
                return Ok($"User {userInfo.Email} was create successfully!");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Register attempt!");
                return BadRequest(ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserTokens>> Login(LoginModel userInfo)
        {
            var result = await _authentication.AuthenticateAsync(userInfo.Email, userInfo.Password);

            if (result)
            {
                return GenerateToken(userInfo);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt!");
                return BadRequest(ModelState);
            }
        }

        private ActionResult<UserTokens> GenerateToken(LoginModel userInfo)
        {
            //user declarations
            var claims = new[]
            {
                new Claim("email",userInfo.Email),
                new Claim("meuvalor", "o que você quiser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //generate private key for to sign the Token
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            //generate the digital signature
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //define the expiration time
            var expiration = DateTime.UtcNow.AddMinutes(10);

            //generate the Token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
                );

            return new UserTokens()
            {
                Expiration = expiration,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
