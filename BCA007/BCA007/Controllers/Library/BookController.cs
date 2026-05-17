using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BCA007.Controllers.Library
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }


        [HttpPost("create")]
        public async Task<ActionResult<BookDto>> Create([FromForm] string dto, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var model = JsonSerializer.Deserialize<BookDto>(dto)!;

                return Ok(await _service.CreateAsync(model, file?.OpenReadStream(), file?.FileName));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("edit")]
        public async Task<ActionResult<BookDto>> Update([FromForm] string dto, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var model = JsonSerializer.Deserialize<BookDto>(dto)!;
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

        [HttpPut("editissue")]
        public async Task<ActionResult<BookDto>> UpdateIssue(BookDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                return Ok(await _service.UpdateIssueAsync(dto));
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