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
    public class RegionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RegionesController> _logger; // Add logger

        public RegionesController(ApplicationDbContext context, ILogger<RegionesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Regiones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Regiones>>> GetRegiones()
        {
            try
            {
                return await _context.Regiones.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET: api/Regiones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Regiones>> GetRegiones(int id)
        {
            try
            {
                var regiones = await _context.Regiones.FindAsync(id);

                if (regiones == null)
                {
                    return NotFound();
                }

                return regiones;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT: api/Regiones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegiones(int id, Regiones regiones)
        {
            try
            {
                if (id != regiones.RegionId)
                {
                    return BadRequest();
                }

                _context.Entry(regiones).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!RegionesExists(id))
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

        // POST: api/Regiones
        [HttpPost]
        public async Task<ActionResult<Regiones>> PostRegiones(Regiones regiones)
        {
            try
            {
                _context.Regiones.Add(regiones);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetRegiones", new { id = regiones.RegionId }, regiones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE: api/Regiones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegiones(int id)
        {
            try
            {
                var regiones = await _context.Regiones.FindAsync(id);
                if (regiones == null)
                {
                    return NotFound();
                }

                _context.Regiones.Remove(regiones);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        private bool RegionesExists(int id)
        {
            return _context.Regiones.Any(e => e.RegionId == id);
        }
    }
}
