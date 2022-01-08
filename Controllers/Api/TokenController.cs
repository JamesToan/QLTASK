using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using coreWeb.Helps;
using coreWeb.Models;
using coreWeb.Models.Entities;
using coreWeb.Services;

namespace coreWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        private ApplicationDbContext _context;
        public TokenController(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            //IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                var u = _context.User.Where(x => x.Id == user.Id).FirstOrDefault();
                var tokenString = BuildToken(u);
                //var refresh_token = Guid.NewGuid().ToString().Replace("-", "");
                return Ok(new
                {
                    Key = tokenString,//, rtoken = refresh_token
                    UserName = u.UserName,
                    Phone = u.Phone
                });
            }

            return Unauthorized();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Signup([FromBody] UserModel userModel)
        {
            //IActionResult response = Unauthorized();
            var objUser = new UserService(_context);
            var result = objUser.Add(userModel.UserName, userModel.Password, userModel.FullName, userModel.Role, null);

            if (result == 1)
            {
                var login = new LoginModel();
                login.UserName = userModel.UserName;
                login.Password = userModel.Password;
                var user = Authenticate(login);

                if (user != null)
                {
                    var tokenString = BuildToken(user);
                    //var refresh_token = Guid.NewGuid().ToString().Replace("-", "");
                    return Ok(new
                    {
                        Key = tokenString,//, rtoken = refresh_token
                        UserName = user.UserName,
                        Phone = user.Phone
                    });
                }
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }
        private string BuildToken(User user)
        {
            string PlainData = string.Format("{0}^{1}", user.Id, user.RoleId);
            string EncodeData = Encryption.Encrypt(PlainData);
            var claims = new[] {
                new Claim("Data", EncodeData),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddDays(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(LoginModel login)
        {
            User _user = null;
            var objUser = new UserService(_context);
            _user = objUser.Login(login.UserName, login.Password);

            if (_user.Id != 0)
            {
                return _user;
            }
            return null;
        }

        public class LoginModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class UserModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string FullName { get; set; }
            public int Role { get; set; }
        }
    }
}
