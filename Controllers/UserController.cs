using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vet_backend.Models;
using Microsoft.EntityFrameworkCore;
using vet_backend.Context;
using Microsoft.AspNetCore.Authorization;

namespace vet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listUsers = await _context.Users.ToListAsync();
                return Ok(listUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get(String username)
        {
            try
            {
                var user = await _context.Users.FindAsync(username);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> Delete(String username)
        {
            try
            {
                var user = await _context.Users.FindAsync(username);
                if (user == null)
                {
                    return NotFound();
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            try
            {
                _context.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Get", new { username = user.UserName }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> Put(String username, User user)
        {
            try
            {
                if (username != user.UserName)
                {
                    return BadRequest();
                }

                var userBase = await _context.Users.FindAsync(username);
                if (user == null)
                {
                    return NotFound();
                }

                userBase.Password = user.Password;
                //userBase.Role = user.Role;
                userBase.Email = user.Email;
                userBase.Nombre = user.Nombre;
                userBase.Apellido = user.Apellido;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
