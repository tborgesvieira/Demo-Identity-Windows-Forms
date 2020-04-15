using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class LoginController : BaseController
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("erro");
        }
        
        [HttpPost]
        public IActionResult Login(UserModel login)
        {
            IActionResult response = Unauthorized();

            if (!ModelState.IsValid)
            {
                response = BadRequest(
                    new
                    {
                        success = false,
                        erros = NotificarErros()
                    });

                return response;
            }

            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { success = true, token = tokenString });
            }
            else
            {
                response = BadRequest(login);
            }

            return response;
        }

        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var key = _config["Jwt:Key"];

            var issuer = _config["Jwt:Issuer"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer,
              issuer,
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = user = new UserModel { Username = Guid.NewGuid().ToString(), EmailAddress = "test.btest@gmail.com" };

            return user;
        }
    }
}