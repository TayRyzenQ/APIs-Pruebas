using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using WebApi29.Context;
using WebApi29.Helpers;
using WebApi29.Services.IServices;

namespace WebApi29.Services.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly ApplicationDbContext _context;
        private readonly JWTHelper _jwt;

        public AuthServices(ApplicationDbContext context, JWTHelper jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        public async Task<AuthResponse> Login(LoginRequest request)
        {
            var usuario = await _context.Usuarios
                 .FirstOrDefaultAsync(u => u.UserName == request.UserName && u.Password == request.Password);

            if (usuario == null) return null;

            var token = _jwt.GenerateToken(usuario);

            return new AuthResponse
            {
                Token = token,
                UserName = usuario.UserName,
                PkUsuario = usuario.PkUsuario
            };
        }
    }
}
