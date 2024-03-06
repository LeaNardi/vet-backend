using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using vet_backend.Models;

namespace vet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MyUserController : ControllerBase
    {
        public UserManager<User> _userManager;
        public RoleManager<IdentityRole> _roleManager;

        public MyUserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            try
            {
                var callerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (id != callerId)
                {
                    return Unauthorized();
                }

                var user = _userManager.FindByIdAsync(id).GetAwaiter().GetResult();

                if (user == null)
                {
                    return NotFound();
                }
                var roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
                var userRed = new
                {
                    id = user.Id,
                    nombre = user.Nombre,
                    apellido = user.Apellido,
                    username = user.UserName,
                    email = user.Email,
                    role = roles[0]
                };
                return Ok(userRed);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(String id, User user)
        {
            try
            {
                var callerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (id != callerId)
                {
                    return Unauthorized();
                }

                if (id != user.Id)
                {
                    return BadRequest();
                }

                var existingUser = _userManager.FindByIdAsync(id).GetAwaiter().GetResult();
                if (existingUser == null)
                {
                    return NotFound();
                }

                existingUser.Nombre = user.Nombre;
                existingUser.Apellido = user.Apellido;
                existingUser.Email = user.Email;
                if (user.Password != "empty")
                {
                    existingUser.Password = user.Password;
                }

                _userManager.UpdateAsync(existingUser).GetAwaiter().GetResult();


                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
