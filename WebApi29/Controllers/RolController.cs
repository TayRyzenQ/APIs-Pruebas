using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi29.Services.IServices;

namespace WebApi29.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RolController : Controller
    {
        private readonly IRolServices _rolServices;

        public RolController(IRolServices rolServices)
        {
            _rolServices = rolServices;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _rolServices.GetAll();
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _rolServices.GetById(id);
            if (response.Result == null)
                return NotFound(response.Message);

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(RolRequest request)
        {
            var response = await _rolServices.Create(request);
            return Ok(response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RolRequest request)
        {
            var response = await _rolServices.Update(id, request);
            if (response.Result == null)
                return NotFound(response.Message);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _rolServices.Delete(id);
            if (response.Result == null)
                return NotFound(response.Message);

            return Ok(response);
        }
    }
}
