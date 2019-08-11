using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MEDAPP.Models.Security;
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
        
        public AuthController(IConfiguration config)//UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
        {
            _config = config;
           /* _userManager = userManager;
            _signInManager = signInManager;*/
        }

        
       /* [HttpPost("register")]
        //[EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> Register([FromBody] Register model)
        {
            var user = new ApplicationUser();
            var result = new IdentityResult();

            try
            {

            //if (model == null)
            //{
                user = new ApplicationUser
                {
                    UserName = "dummyAdmin",
                    Email = "dummyAdmin@dummyAdmin.com",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                result = await _userManager.CreateAsync(user, "dummyAdmin");
            //}
            
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            return Ok(
                new
                {
                    Username = user.UserName

                });

            }
            catch (Exception e)
            {

                throw;
            }
        }*/


        [HttpPost("login")]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult Login([FromBody] Object model)
        {

           // var user = await _userManager.FindByNameAsync(model.UserName);
           // if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            //{
                //var claim = new[]
                //{
                   // new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
               // };

                //generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, "Lawrence")
                }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var tokenStr = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(tokenStr);

                return Ok(new { token });
            //}

            //return Unauthorized();
           
        }
    }
}