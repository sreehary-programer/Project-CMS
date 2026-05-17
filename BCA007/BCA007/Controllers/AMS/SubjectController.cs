using BCA007.Shared.DTOs;
using BCA007.Shared.Service.AMS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BCA007.Controllers.CMS
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {

        private readonly ISubjectService _serv;

        public SubjectController(ISubjectService serv)
        {
            _serv = serv;
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _serv.GetAllAsync());
        }

        [HttpPost("create")]
        public async Task<ActionResult<SubjectDto>> CreateAsync(SubjectDto dto)
        {
            return Ok(await _serv.CreateAsync(dto));
        }
        [HttpPut("edit")]
        public async Task<ActionResult<SubjectDto>> Update(SubjectDto dto)
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

                return Conflict(ex.Message);
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
