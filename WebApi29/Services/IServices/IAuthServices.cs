using Domain.DTO;
using Domain.Entities;

namespace WebApi29.Services.IServices
{
    public interface IAuthServices
    {
        Task<AuthResponse> Login(LoginRequest request);
    }
}
