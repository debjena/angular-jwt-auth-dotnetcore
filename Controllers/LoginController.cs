using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using angular_jwt_auth_dotnetcore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace angular_jwt_auth_dotnetcore.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {
        private readonly UserDbContext _userdbcontext;
        public LoginController (UserDbContext userDbContext) {
            _userdbcontext = userDbContext;
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("user is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var _user = (from u in _userdbcontext.Users where (u.UserName == user.UserName && u.Password == user.Password) select u);
                if (_user.Count() != 0)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DebKey@321@secret"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(3),
                        signingCredentials: signinCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new { access_token = tokenString });
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}