using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Business.Abstract;
using ToDoApp.Entities.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _service.GetByIdAsync(id);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserDto dto)
        {
            var res = await _service.AddAsync(dto);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
    }
}
