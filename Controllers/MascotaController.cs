using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vet_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace vet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MascotaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listMascotas = await _context.Mascotas.ToListAsync();
                return Ok(listMascotas);
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
