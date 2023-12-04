//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalDotNet8.Client;
using ProyectoFinalDotNet8.Data;
using Shared.Models;

namespace ProyectoFinalDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUser()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("RolesDirector")]
        public async Task<ActionResult<IEnumerable<String>>> GetRole()
        {
            var Director = (await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == "Director"))!.Id;
            var roleUser = await _context.UserRoles
                .Where(r => r.RoleId == Director)
                .ToListAsync();
            var Directores = roleUser.Select(r => r.UserId).ToList();
           
            return Directores!;
        }
        [HttpGet("ObtenerRole")]
        public async Task<ActionResult<String>> GetRole(string UserId)
        {
            var roleUser = await _context.UserRoles
                .Where(r => r.UserId == UserId)
                .ToListAsync();
            var Roles = roleUser.Select(r => r.RoleId).ToList();
            return Roles[0];
        }
        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetUser(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("UserRole")]
        public async Task<ActionResult<ApplicationUser>> GetUserRole()
        {
            var userRoles = await _context.UserRoles.ToListAsync();
            return Ok(userRoles);
        }

        [HttpGet("Roles")]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, ApplicationUser User)
        {
            if (id != User.Id)
            {
                return BadRequest();
            }

            _context.Entry(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Menus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> PostMenu(ApplicationUser user)
        {
            if (!UserExists(user.Id))
                _context.Users.Add(user);
            else
                _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
