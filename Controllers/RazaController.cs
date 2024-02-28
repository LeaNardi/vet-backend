using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vet_backend.Context;

namespace vet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RazaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RazaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listRazas = await _context.Razas
                    .ToListAsync();

                return Ok(listRazas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
