using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Add this namespace
using ProyectoFinalDotNet8.Data;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UsuersController> _logger; // Add logger

        public UsuersController(ApplicationDbContext context, ILogger<UsuersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUser()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("RolesDirector")]
        public async Task<ActionResult<IEnumerable<string>>> GetRole()
        {
            try
            {
                var Director = (await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Director"))?.Id;
                var roleUser = await _context.UserRoles.Where(r => r.RoleId == Director).ToListAsync();
                var Directores = roleUser.Select(r => r.UserId).ToList();
                return Directores!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("ObtenerRole")]
        public async Task<ActionResult<string>> GetRole(string UserId)
        {
            try
            {
                var roleUser = await _context.UserRoles.Where(r => r.UserId == UserId).ToListAsync();
                var Roles = roleUser.Select(r => r.RoleId).ToList();
                return Roles[0];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetUser(string id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == id);

                if (user == null)
                {
                    return NotFound();
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("UserRole")]
        public async Task<ActionResult<IEnumerable<IdentityUserRole<string>>>> GetUserRole()
        {
            try
            {
                var userRoles = await _context.UserRoles.ToListAsync();
                return Ok(userRoles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("Roles")]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetRoles()
        {
            try
            {
                return await _context.Roles.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, ApplicationUser User)
        {
            try
            {
                if (id != User.Id)
                {
                    return BadRequest();
                }

                _context.Entry(User).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UserExists(id))
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

        // POST: api/Usuers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> PostUser(ApplicationUser user)
        {
            try
            {
                if (!UserExists(user.Id))
                    _context.Users.Add(user);
                else
                    _context.Users.Update(user);

                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
