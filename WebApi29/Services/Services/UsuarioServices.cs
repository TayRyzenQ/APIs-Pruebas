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
        private readonly ApplicationDbContext _context;   //el guion bajo es por la proteccion
        public UsuarioServices(ApplicationDbContext context)
        {
            _context = context;
        }

        //Lista de usuarios
        public async Task<Response<List<Usuario>>> GetAll()
        {
            try
            {
                List<Usuario> response = await _context.Usuarios.Include(x=> x.Roles).ToListAsync();
                return new Response<List<Usuario>>(response, "Lista de Usuarios");

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        // Obtener Usuario
        public async Task<Response<Usuario>> GetById(int id)
        {
            try
            {
                // Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.PkUsuario == id);

                Usuario usuario = await _context.Usuarios.FindAsync(id);      //esta es otra forma de hacer la busqueda por el id
                return new Response<Usuario>(usuario);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error " + ex.Message);
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

                return new Response<Usuario>(usuario1);
            }
            catch (Exception)
            {

                throw;
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

                throw new Exception("Ocurrió un error: " + ex.Message);
            }
        }

        public async Task<Response<Usuario>> Update(int id, Usuario UpdUser)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null) { return new Response<Usuario>(null, "Usuario no encontrado");  }

                usuario.Nombre = UpdUser.Nombre;
                usuario.Password = UpdUser.Password;
                usuario.UserName = UpdUser.UserName;
                usuario.FkRol = UpdUser.FkRol;

                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();

                return new Response<Usuario>(usuario, "Usuario actualizado correctamente");
                    
                
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
//inv q es dto, que es jwt token, que hace asp .net core, que es programacion asincrona