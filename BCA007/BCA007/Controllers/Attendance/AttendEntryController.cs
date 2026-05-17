using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Attendance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BCA007.Controllers.Attendance
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendEntryController : ControllerBase
    {
        private readonly IAttendEntryService _serv;

        public AttendEntryController(IAttendEntryService serv)
        {
            _serv = serv;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _serv.GetAllAsync());
        }
        [HttpPost("create")]
        public async Task<ActionResult<AttendEntryDto>> Create(AttendEntryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _serv.CreateAsync(dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
        [HttpPut("edit")]
        public async Task<ActionResult<AttendEntryDto>> Update(AttendEntryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                return Ok(await _serv.UpdateAsync(dto));
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
            try
            {
                await _serv.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("byattend/{FrmId:int}")]

        public async Task<IActionResult> GetAttendanceById(int? FrmId)
        {
            if (FrmId <= 0)
                return BadRequest("Invalid  Id.");

            var results = await _serv.GetAttendanceById(FrmId);

            if (results == null || !results.Any())
                return NotFound("No results found for this .");

            return Ok(results);
        }

    }
}
