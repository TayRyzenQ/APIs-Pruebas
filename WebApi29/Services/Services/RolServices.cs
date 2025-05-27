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
            try
            {
                var roles = await _context.Roles.ToListAsync();
                return new Response<List<Rol>>(roles, "Lista de roles");
            }
            catch (Exception ex)
            {
                return new Response<List<Rol>>(null, $"Error al obtener roles: {ex.Message}");      //se le especifica al usuario que es que salio mal
            }
        }

        public async Task<Response<Rol>> GetById(int id)
        {
            try
            {
                var rol = await _context.Roles.FindAsync(id);
                if (rol == null)
                    return new Response<Rol>(null, "Rol no encontrado");

                return new Response<Rol>(rol);
            }
            catch (Exception ex)
            {
                return new Response<Rol>(null, $"Error al obtener el rol: {ex.Message}");           //se le especifica al usuario que es que salio mal
            }
        }

        public async Task<Response<Rol>> Create(RolRequest request)
        {
            try
            {
                var rol = new Rol { Nombre = request.Nombre };
                _context.Roles.Add(rol);
                await _context.SaveChangesAsync();

                return new Response<Rol>(rol, "Rol creado exitosamente");
            }
            catch (Exception ex)
            {
                return new Response<Rol>(null, $"Error al crear el rol: {ex.Message}");    //se le especifica al usuario que es que salio mal
            }
        }

        public async Task<Response<Rol>> Update(int id, RolRequest request)
        {
            try
            {
                var rol = await _context.Roles.FindAsync(id);
                if (rol == null)
                    return new Response<Rol>(null, "Rol no encontrado");    

                rol.Nombre = request.Nombre;
                _context.Roles.Update(rol);
                await _context.SaveChangesAsync();

                return new Response<Rol>(rol, "Rol actualizado correctamente");
            }
            catch (Exception ex)
            {
                return new Response<Rol>(null, $"Error al actualizar el rol: {ex.Message}");  //se le especifica al usuario que es que salio mal
            }
        }

        public async Task<Response<Rol>> Delete(int id)
        {
            try
            {
                var rol = await _context.Roles.FindAsync(id);
                if (rol == null)
                    return new Response<Rol>(null, "Rol no encontrado");

                _context.Roles.Remove(rol);
                await _context.SaveChangesAsync();

                return new Response<Rol>(rol, "Rol eliminado correctamente");
            }
            catch (Exception ex)
            {
                return new Response<Rol>(null, $"Error al eliminar el rol: {ex.Message}");      //se le especifica al usuario que es que salio mal
            }
        }
    }
}
