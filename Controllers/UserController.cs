using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vet_backend.Models;
using Microsoft.EntityFrameworkCore;
using vet_backend.Context;

namespace vet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
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
                user.FechaCreacion = DateTime.Now;
                _context.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Get", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, User user)
        {
            try
            {
                if (id != user.Id)
                {
                    return BadRequest();
                }

                var userBase = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                userBase.UserName = user.UserName;
                userBase.Password = user.Password;
                userBase.Role = user.Role;
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
