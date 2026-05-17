using System.Text.Json;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BCA007.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _service;

        public StaffController(IStaffService service)
        {
            _service = service;
        }
        [HttpPost("create")]
        public async Task<ActionResult<StaffDto>> Create([FromForm] string dto, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var model = JsonSerializer.Deserialize<StaffDto>(dto)!;

                return Ok(await _service.CreateAsync(model, file?.OpenReadStream(), file?.FileName));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("edit")]
        public async Task<ActionResult<StaffDto>> Update([FromForm] string dto, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var model = JsonSerializer.Deserialize<StaffDto>(dto)!;
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
        [HttpPut("{id}/payment-status")]
        public async Task<IActionResult> UpdatePaymentStatus(int id, [FromBody] StaffPaymentStatusUpdateDto dto)
        {
            await _service.UpdatePaymentStatusAsync(id, dto.Status, dto.DueDate);
            return Ok();
        }

    }
}

