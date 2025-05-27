using Domain.DTO;
using Domain.Entities;
using WebApi29.Context;
using Microsoft.EntityFrameworkCore;
using WebApi29.Services.IServices;

namespace WebApi29.Services.Services
{
    public class RolServices : IRolServices
    {
        private readonly ApplicationDbContext _context;

        public RolServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Rol>>> GetAll()
        {
            var roles = await _context.Roles.ToListAsync();
            return new Response<List<Rol>>(roles, "Lista de roles");
        }

        public async Task<Response<Rol>> GetById(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
                return new Response<Rol>(null, "Rol no encontrado");

            return new Response<Rol>(rol);
        }

        public async Task<Response<Rol>> Create(RolRequest request)
        {
            var rol = new Rol { Nombre = request.Nombre };
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();

            return new Response<Rol>(rol, "Rol creado exitosamente");
        }

        public async Task<Response<Rol>> Update(int id, RolRequest request)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
                return new Response<Rol>(null, "Rol no encontrado");

            rol.Nombre = request.Nombre;
            _context.Roles.Update(rol);
            await _context.SaveChangesAsync();

            return new Response<Rol>(rol, "Rol actualizado correctamente");
        }

        public async Task<Response<Rol>> Delete(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
                return new Response<Rol>(null, "Rol no encontrado");

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();

            return new Response<Rol>(rol, "Rol eliminado correctamente");
        }
    }
}
