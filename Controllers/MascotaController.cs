    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vet_backend.Models;
using Microsoft.EntityFrameworkCore;
using vet_backend.Context;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;

namespace vet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
                var listMascotas = await _context.Mascotas
                    .Include(mascota => mascota.Raza)
                    .Include(mascota => mascota.Color)
                    .ToListAsync();
                var nuevolistado = listMascotas.Select(m => new{ 
                    id = m.MascotaId, 
                    nombre = m.Nombre, 
                    raza = m.Raza.RazaNombre,
                    color = m.Color.ColorNombre
                });

                return Ok(nuevolistado);
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var mascota = await _context.Mascotas
                    .Include(mascota => mascota.Raza)
                    .Include(mascota => mascota.Color)
                    .SingleOrDefaultAsync(m => m.MascotaId == id);
                if(mascota == null)
                {
                    return NotFound();
                }
                var nuevaMascota = new { 
                    id = mascota.MascotaId, 
                    nombre = mascota.Nombre,
                    raza = mascota.Raza.RazaNombre,
                    color = mascota.Color.ColorNombre,
                    edad = mascota.Edad,
                    peso = mascota.Peso
                };
                return Ok(nuevaMascota);
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
                var mascota = await _context.Mascotas.FindAsync(id);
                if (mascota == null)
                {
                    return NotFound();
                }
                _context.Mascotas.Remove(mascota);
                await _context.SaveChangesAsync();
                return Ok(mascota);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Mascota mascota)
        {
            try
            {
                mascota.FechaCreacion = DateTime.Now;
                _context.Add(mascota);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Get", new { id = mascota.MascotaId }, mascota);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Mascota mascota)
        {
            try
            {
                if(id != mascota.MascotaId)
                {
                    return BadRequest();
                }

                var mascotaBase = await _context.Mascotas.FindAsync(id);
                if (mascota == null)
                {
                    return NotFound();
                }

                mascotaBase.Nombre = mascota.Nombre;
                mascotaBase.Raza = mascota.Raza;
                mascotaBase.Edad = mascota.Edad;
                mascotaBase.Peso = mascota.Peso;
                mascotaBase.Color = mascota.Color;

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
