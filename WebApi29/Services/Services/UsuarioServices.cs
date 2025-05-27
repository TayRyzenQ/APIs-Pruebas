using Azure.Core;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi29.Context;
using WebApi29.Services.IServices;
using WebApi29.Services.Services;

namespace WebApi29.Services.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly ApplicationDbContext _context;   // el guion bajo es por la proteccion

        public UsuarioServices(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista de usuarios
        public async Task<Response<List<Usuario>>> GetAll()
        {
            try
            {
                List<Usuario> response = await _context.Usuarios.Include(x => x.Roles).ToListAsync();
                return new Response<List<Usuario>>(response, "Lista de Usuarios");
            }
            catch (Exception ex)
            {
                return new Response<List<Usuario>>(null, "Ocurrió un error al obtener los usuarios: " + ex.Message);                    //se le especifica al usuario que es que salio mal
            }
        }

        // Obtener Usuario
        public async Task<Response<Usuario>> GetById(int id)
        {
            try
            {
                // Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.PkUsuario == id);

                Usuario usuario = await _context.Usuarios.FindAsync(id);      // esta es otra forma de hacer la búsqueda por el id

                if (usuario == null)
                    return new Response<Usuario>(null, "Usuario no encontrado");

                return new Response<Usuario>(usuario);
            }
            catch (Exception ex)
            {
                return new Response<Usuario>(null, "Ocurrió un error al obtener el usuario: " + ex.Message);                        //se le especifica al usuario que es que salio mal
            }
        }

        public async Task<Response<Usuario>> Create(UsuarioRequest request)
        {
            try
            {
                Usuario usuario1 = new Usuario()
                {
                    Nombre = request.Nombre,
                    Password = request.Password,
                    UserName = request.UserName,
                    FkRol = request.FkRol,
                };

                _context.Usuarios.Add(usuario1);
                await _context.SaveChangesAsync();

                return new Response<Usuario>(usuario1, "Usuario creado correctamente");
            }
            catch (Exception ex)
            {
                return new Response<Usuario>(null, "Ocurrió un error al crear el usuario: " + ex.Message);    //se le especifica al usuario que es que salio mal
            }
        }

        public async Task<Response<Usuario>> Delete(int id)
        {
            try
            {
                Usuario usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    return new Response<Usuario>(null, "Usuario no encontrado");
                }

                _context.Usuarios.Remove(usuario); // Eliminar de la BD
                await _context.SaveChangesAsync();

                return new Response<Usuario>(usuario, "Usuario eliminado correctamente");
            }
            catch (Exception ex)
            {
                return new Response<Usuario>(null, "Ocurrió un error al eliminar el usuario: " + ex.Message);               //se le especifica al usuario que es que salio mal
            }
        }

        public async Task<Response<Usuario>> Update(int id, Usuario UpdUser)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    return new Response<Usuario>(null, "Usuario no encontrado");
                }

                usuario.Nombre = UpdUser.Nombre;
                usuario.Password = UpdUser.Password;
                usuario.UserName = UpdUser.UserName;
                usuario.FkRol = UpdUser.FkRol;

                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();

                return new Response<Usuario>(usuario, "Usuario actualizado correctamente");
            }
            catch (Exception ex)
            {
                return new Response<Usuario>(null, "Ocurrió un error al actualizar el usuario: " + ex.Message);     //se le especifica al usuario que es que salio mal
            }
        }
    }
}
