using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using vet_backend.Context;
using vet_backend.Models;
using Microsoft.AspNetCore.Identity;
using vet_backend.Helpers;


namespace vet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        IServiceProvider _serviceProvider;
        IServiceScope _scope;

        public AuthenticationController(ApplicationDbContext context, IConfiguration config, IServiceProvider serviceProvider)
        {
            _context = context;
            _config = config;
            _serviceProvider = serviceProvider;
            _scope = _serviceProvider.CreateScope();
            Console.WriteLine("AuthenticationController started");

        }


        [HttpPost("authenticate")]
        public ActionResult<string> Autenticar(AuthenticationRequestBody authRequestBody)
        {
            var user = _context.Users.FirstOrDefault(p => p.UserName == authRequestBody.UserName && p.Password == authRequestBody.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));
            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);
            var _userManager = _scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString()));
            claimsForToken.Add(new Claim("given_name", user.Nombre));
            claimsForToken.Add(new Claim("family_name", user.Apellido));

            //Roles
            var usuario = _userManager.FindByNameAsync(authRequestBody.UserName).GetAwaiter().GetResult();
            //Console.WriteLine($"usuario: {usuario}");
            var roles = _userManager.GetRolesAsync(usuario).GetAwaiter().GetResult();
            foreach (var role in roles)
            {
                claimsForToken.Add(new Claim(ClaimTypes.Role, role));
                //Console.WriteLine($"role: {role}");
            }

            var jwtSecurityToken = new JwtSecurityToken(
              _config["Authentication:Issuer"],
              _config["Authentication:Audience"],
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddHours(1),
              credentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }


    }
}
