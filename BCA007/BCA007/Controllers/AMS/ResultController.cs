using BCA007.Shared.DTOs.AMS;
using BCA007.Shared.Service.AMS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BCA007.Controllers.CMS
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _service;

        public ResultController(IResultService service)
        {
            _service = service;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }
       
        [HttpGet("byexam/{examId:int}")]
        public async Task<IActionResult> GetResultsByExamId(int? examId)
        {
            if (examId <= 0)
                return BadRequest("Invalid Exam Id.");

            var results = await _service.GetResultsByExamId(examId);

            if (results == null || !results.Any())
                return NotFound("No results found for this exam.");

            return Ok(results);
        }
        [HttpPut("save")]
        public async Task<IActionResult> UpdateResults([FromBody] List<ResultViewDto> dto)
        {
            if (dto == null || !dto.Any())
                return BadRequest("Invalid data.");

            await _service.UpdateResults(dto);

            return Ok("Updated successfully.");
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
