using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Add this namespace
using ProyectoFinalDotNet8.Data;
using Shared.Models;

namespace ProyectoFinalDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistritosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DistritosController> _logger; // Add logger

        public DistritosController(ApplicationDbContext context, ILogger<DistritosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Distritos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Distritos>>> GetDistritos()
        {
            try
            {
                return await _context.Distritos.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET: api/Distritos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Distritos>> GetDistritos(int id)
        {
            try
            {
                var distritos = await _context.Distritos.FindAsync(id);

                if (distritos == null)
                {
                    return NotFound();
                }

                return distritos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT: api/Distritos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDistritos(int id, Distritos distritos)
        {
            try
            {
                if (id != distritos.DistritoId)
                {
                    return BadRequest();
                }

                _context.Entry(distritos).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!DistritosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, ex.Message);
                    return StatusCode(500, "Internal Server Error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // POST: api/Distritos
        [HttpPost]
        public async Task<ActionResult<Distritos>> PostDistritos(Distritos distritos)
        {
            try
            {
                _context.Distritos.Add(distritos);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetDistritos", new { id = distritos.DistritoId }, distritos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE: api/Distritos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistritos(int id)
        {
            try
            {
                var distritos = await _context.Distritos.FindAsync(id);
                if (distritos == null)
                {
                    return NotFound();
                }

                _context.Distritos.Remove(distritos);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        private bool DistritosExists(int id)
        {
            return _context.Distritos.Any(e => e.DistritoId == id);
        }
    }
}
