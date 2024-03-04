using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using vet_backend.Context;
using vet_backend.Models;

namespace vet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class UserASPController : ControllerBase
    {
        public UserManager<User> _userManager;
        public RoleManager<IdentityRole> _roleManager;

        public UserASPController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listUsersASP = _userManager.Users.ToListAsync().GetAwaiter().GetResult();
                List< UserResponse> filteredUsers = new List<UserResponse>();

                listUsersASP.ForEach(user => {
                    var roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
                    filteredUsers.Add(
                    new UserResponse
                    {
                        id = user.Id,
                        nombre = user.Nombre,
                        apellido = user.Apellido,
                        username = user.UserName,
                        email = user.Email,
                        role = roles[0],
                    }
                    );
            });
                return Ok(filteredUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            try
            {
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id)
        {
            try
            {
                var user = _userManager.FindByIdAsync(id).GetAwaiter().GetResult();
                if (user == null)
                {
                    return NotFound();
                }
                var result = _userManager.DeleteAsync(user).GetAwaiter().GetResult();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user, string role)
        {
            try
            {
                user = new User { UserName = user.UserName, Email = user.Email, Nombre = user.Nombre, Apellido = user.Apellido, Password = user.Password };
                var usuarioexiste = _userManager.FindByNameAsync(user.UserName).GetAwaiter().GetResult();
                if (usuarioexiste == null)
                {
                    _userManager.CreateAsync(user, user.Password).GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(user, role).GetAwaiter().GetResult();
                }

                return CreatedAtAction("Get", new { username = user.UserName }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(String id, User user, string role)
        {
            try
            {
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


                var roles = _userManager.GetRolesAsync(existingUser).GetAwaiter().GetResult();
                _userManager.RemoveFromRolesAsync(existingUser, roles).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(existingUser, role).GetAwaiter().GetResult();


                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
