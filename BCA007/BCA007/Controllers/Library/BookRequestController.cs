using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace BCA007.Controllers.Library
{

    [Route("api/[controller]")]
    [ApiController]
    public class BookRequestController : ControllerBase
    {
        private readonly IBookRequestService _serv;

        public BookRequestController(IBookRequestService serv)
        {
            _serv = serv;
        }
    

         [HttpPost("create")]
        public async Task<ActionResult<BookRequestDto>> Create(BookRequestDto dto)
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
        public async Task<ActionResult<BookRequestDto>> Update(BookRequestDto dto)
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
    } 
}
