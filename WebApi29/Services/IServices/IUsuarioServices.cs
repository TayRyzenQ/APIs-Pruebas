using Domain.DTO;
using Domain.Entities;

namespace WebApi29.Services.IServices
{
    public interface IUsuarioServices
    {
        public Task<Response<List<Usuario>>> GetAll();
        public Task<Response<Usuario>> GetById(int request);
        public Task<Response<Usuario>> Create(UsuarioRequest request);
        public Task<Response<Usuario>> Delete(int id);
        public Task<Response<Usuario>> Update(int id, Usuario UpdUser);


    }
}
