using System.Text.Json;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BCA007.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }
        [HttpPost("create")]
        public async Task<ActionResult<AdminDto>> Create([FromForm] string dto, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var model = JsonSerializer.Deserialize<AdminDto>(dto)!;

                return Ok(await _service.CreateAsync(model, file?.OpenReadStream(), file?.FileName));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("edit")]
        public async Task<ActionResult<AdminDto>> Update([FromForm] string dto, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var model = JsonSerializer.Deserialize<AdminDto>(dto)!;
                return Ok(await _service.UpdateAsync(model, file?.OpenReadStream(), file?.FileName));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // 409
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }
    }
}
