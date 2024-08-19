using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Business.Abstract;
using ToDoApp.Entities.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        IToDoItemService _service;
        public ToDoItemsController(IToDoItemService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _service.GetAllAsync();
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _service.GetByIdAsync(id);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ToDoItemInsertDto dto)
        {
            var res = await _service.AddAsync(dto);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        [HttpPut]
        public async Task<IActionResult> Update(ToDoItemDto dto)
        {
            var res = await _service.UpdateAsync(dto);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _service.RemoveAsync(id);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
    }
}
