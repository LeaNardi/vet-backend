using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vet_backend.Context;
using vet_backend.Models;

namespace vet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HistoriaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HistoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var historias = _context.Historias.Where(e => e.MascotaId == id);

                return Ok(historias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id, Historia historia)
        {
            try
            {
                historia.Fecha = DateTime.Now;
                historia.MascotaId = id;
                _context.Add(historia);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Get", new { id = historia.HistoriaId }, historia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
