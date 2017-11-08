using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BookStore.Auth;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Identity.API.DataModel.Entities;
using Identity.API.Model;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Identity.API.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly JwtBearerOptions jwtOptions;
        private readonly SecurityTokenOptions securityTokenOptions;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;

        public AccountController(
            IOptions<JwtBearerOptions> jwtOptions,
            IOptions<SecurityTokenOptions> securityTokenOptions,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            this.jwtOptions = jwtOptions.Value;
            this.securityTokenOptions = securityTokenOptions.Value;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                return BadRequest();
            }

            var claims = (await signInManager.CreateUserPrincipalAsync(user)).Claims;
            var token = jwtSecurityTokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Audience = securityTokenOptions.Audience,
                EncryptingCredentials = securityTokenOptions.EncryptingCredentials,
                Issuer = securityTokenOptions.Issuer,
                SigningCredentials = securityTokenOptions.SigningCredentials,
                Subject = new ClaimsIdentity(claims)
            });
            var accessToken = jwtSecurityTokenHandler.WriteToken(token);

            return Ok(accessToken);
        }
    }
}