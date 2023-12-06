using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalDotNet8.Data;
using Shared.Models;

namespace ProyectoFinalDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComidasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ComidasController> _logger;

        public ComidasController(ApplicationDbContext context, ILogger<ComidasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Comidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comidas>>> GetComidas()
        {
            try
            {
                return await _context.Comidas.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET: api/Comidas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comidas>> GetComidas(int id)
        {
            try
            {
                var comidas = await _context.Comidas.FindAsync(id);

                if (comidas == null)
                {
                    return NotFound();
                }

                return comidas;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT: api/Comidas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComidas(int id, Comidas comidas)
        {
            try
            {
                if (id != comidas.ComidaId)
                {
                    return BadRequest();
                }

                _context.Entry(comidas).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ComidasExists(id))
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

        // POST: api/Comidas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comidas>> PostComidas(Comidas comidas)
        {
            try
            {
                if (!ComidasExists(comidas.ComidaId))
                    _context.Comidas.Add(comidas);
                else
                    _context.Comidas.Update(comidas);

                await _context.SaveChangesAsync();

                return Ok(comidas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE: api/Comidas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComidas(int id)
        {
            try
            {
                var comidas = await _context.Comidas.FindAsync(id);
                if (comidas == null)
                {
                    return NotFound();
                }

                _context.Comidas.Remove(comidas);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        private bool ComidasExists(int id)
        {
            return _context.Comidas.Any(e => e.ComidaId == id);
        }
    }

}





