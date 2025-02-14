using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;
using System.Security.Cryptography;
using System.Text;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TaskManagerDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(TaskManagerDbContext context, JwtService jwtService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = new Usuario
            {
                Nombre = model.Nombre,
                Email = model.Email,
                PasswordHash = CreatePasswordHash(model.Password),
                FechaRegistro = DateTime.UtcNow
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuario registrado correctamente" });
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var usuario = await _context.Usuarios.SingleOrDefaultAsync(u => u.Email == model.Email);

            if (usuario == null || !VerifyPasswordHash(model.Password, usuario.PasswordHash))
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            var token = _jwtService.GenerateToken(usuario.IdUsuario, usuario.Email);
            return Ok(new { Token = token });
        }

        private byte[] CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash)
        {
            using (var hmac = new HMACSHA512())
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}
