using Azure;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi29.Services.IServices;
using WebApi29.Services.Services;

namespace WebApi29.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]     //rutea el controlador para q aparexca en el swagger

    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioservices;

        public UsuarioController(IUsuarioServices usuarioservices)
        {
            _usuarioservices = usuarioservices;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _usuarioservices.GetAll();
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _usuarioservices.GetById(id);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(UsuarioRequest request)
        {
            var respose = await _usuarioservices.Create(request);
            return Ok(respose);

        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        { 
            var response = await _usuarioservices.Delete(id);
            return Ok(response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Usuario usuario)
        {
            var response = await _usuarioservices.Update(id, usuario);
            if (response.Result == null)
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }

    }
}
