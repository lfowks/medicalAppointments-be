using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MEDAPP.Models.Security;
using MEDAPP.Services;
using MEDAPP.WebAPI.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MEDAPP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly ISecurityService _svSecurity;


        public AuthController(IConfiguration config, ISecurityService security)//UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
        {
            _config = config;
            _svSecurity = security;
            /* _userManager = userManager;
             _signInManager = signInManager;*/
        }

        
        [HttpPost("register")]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> Register([FromBody] User model)
        {
            var passwordHash = _config.GetSection("AppSettings:Hash").Value;

            model.PasswordHash = passwordHash;
            
            await _svSecurity.CreateUserAsync<User>(model);
            await _svSecurity.CreateUserRoleAsync<UserRole>(new UserRole
                    {
                        UserId = model.Id,
                        RoleId =int.Parse(model.RoleSelected)
            });
            

            if (model.Id!=0)
            {
               return Ok(
               new
               {
                   Username = model.UserName

               });
            }

             return Unauthorized();
        }


        [HttpPost("login")]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<IActionResult> Login([FromBody] User model)
        {
            var passwordHash = _config.GetSection("AppSettings:Hash").Value;

            model.PasswordHash = passwordHash;

            User userAuth = await _svSecurity.CheckUserAndPassword(model);

            if (userAuth != null)
            {

                List<Claim> listClaims = new List<Claim>();

                listClaims.Add(new Claim(ClaimTypes.Name, userAuth.UserName));
                userAuth.Roles.ToList().ForEach(
                 x => listClaims.Add(new Claim(ClaimTypes.Role, x.Name))
                );

              //generate token
              var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(listClaims.ToArray()),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var tokenStr = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(tokenStr);

                return Ok(
                    new {
                         token,
                         User=userAuth
                    });

            }
            //}

            return Unauthorized();

        }
    }
}