using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BCA007.Controllers.Library
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookIssueController : ControllerBase
    {
        private readonly IBookIssueViewService _servbookissue;

        public BookIssueController(IBookIssueViewService servbookissue)
        {
            _servbookissue = servbookissue;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _servbookissue.GetAllAsync());
        }
        [HttpPost("create")]
        public async Task<ActionResult<BookTypeDto>> Create(BookIssueDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _servbookissue.CreateAsync(dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
        [HttpPut("edit")]
        public async Task<ActionResult<BookTypeDto>> Update(BookIssueDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                return Ok(await _servbookissue.UpdateAsync(dto));
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
                await _servbookissue.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

