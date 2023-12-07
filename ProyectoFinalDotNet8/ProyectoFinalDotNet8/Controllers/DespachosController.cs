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
    public class DespachosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DespachosController> _logger; // Add logger

        public DespachosController(ApplicationDbContext context, ILogger<DespachosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Despachos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Despacho>>> GetDespachos()
        {
            try
            {
                return await _context.Despachos.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET: api/Despachos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Despacho>> GetDespacho(int id)
        {
            try
            {
                var despacho = await _context.Despachos
                    .Include(Dd => Dd.DespachoDetalles)
                    .Where(d => d.DespachoId == id)
                    .FirstOrDefaultAsync();

                if (despacho == null)
                {
                    return NotFound();
                }

                return despacho;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT: api/Despachos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDespacho(int id, Despacho despacho)
        {
            try
            {
                if (id != despacho.DespachoId)
                {
                    return BadRequest();
                }

                _context.Entry(despacho).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!DespachoExists(id))
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

        // POST: api/Despachos
        [HttpPost]
        public async Task<ActionResult<Despacho>> PostDespacho(Despacho despacho)
        {
            try
            {
                if (!DespachoExists(despacho.DespachoId))
                    _context.Despachos.Add(despacho);
                else
                    _context.Despachos.Update(despacho);

                await _context.SaveChangesAsync();

                return Ok(despacho);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE: api/Despachos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDespacho(int id)
        {
            try
            {
                var despacho = await _context.Despachos.FindAsync(id);
                if (despacho == null)
                {
                    return NotFound();
                }

                _context.Despachos.Remove(despacho);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        private bool DespachoExists(int id)
        {
            return _context.Despachos.Any(e => e.DespachoId == id);
        }
    }
}
