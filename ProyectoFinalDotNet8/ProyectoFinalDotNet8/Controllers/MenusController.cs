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
    public class MenusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MenusController> _logger; // Add logger

        public MenusController(ApplicationDbContext context, ILogger<MenusController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Menus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            try
            {
                if (_context.Menus == null)
                {
                    return NotFound();
                }
                return await _context.Menus.Include(m => m.MenuDetalles).Include(m => m.ComenentarioDetalle).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET: api/Menus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            try
            {
                if (_context.Menus == null)
                {
                    return NotFound();
                }
                var menu = await _context.Menus.Include(m => m.MenuDetalles).Include(m => m.ComenentarioDetalle)
                    .Where(m => m.MenuId == id)
                    .FirstOrDefaultAsync();

                if (menu == null)
                {
                    return NotFound();
                }

                return menu;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT: api/Menus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(int id, Menu menu)
        {
            try
            {
                if (id != menu.MenuId)
                {
                    return BadRequest();
                }

                _context.Entry(menu).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!MenuExists(id))
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

        // POST: api/Menus
        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(Menu menu)
        {
            try
            {
                if (!MenuExists(menu.MenuId))
                    _context.Menus.Add(menu);
                else
                    _context.Menus.Update(menu);

                await _context.SaveChangesAsync();

                return Ok(menu);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("Comentarios")]
        public async Task<ActionResult<Menu>> PostComentarios(Comentarios comentarios)
        {
            try
            {
                if (!ComentarioExists(comentarios.ComentarioId))
                    _context.Comentarios.Add(comentarios);
                else
                    _context.Comentarios.Update(comentarios);

                await _context.SaveChangesAsync();

                return Ok(comentarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE: api/Menus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            try
            {
                var menu = await _context.Menus.FindAsync(id);
                if (menu == null)
                {
                    return NotFound();
                }

                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.MenuId == id);
        }

        private bool ComentarioExists(int id)
        {
            return _context.Comentarios.Any(e => e.ComentarioId == id);
        }

        [HttpGet("Comentarios")]
        public async Task<ActionResult<IEnumerable<Comentarios>>> GetComentarios()
        {
            try
            {
                if (_context.Comentarios == null)
                {
                    return NotFound();
                }
                return await _context.Comentarios.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
