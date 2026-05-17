using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.AMS;
using BCA007.Shared.Service.AMS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BCA007.Controllers.CMS
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
            private readonly IActivitiesService _serv;

            public ActivitiesController(IActivitiesService serv)
            {
                _serv = serv;
            }
            [HttpGet("getall")]
            public async Task<IActionResult> GetAllAsync()
            {
                return Ok(await _serv.GetAllAsync());
            }
            [HttpPost("create")]
            public async Task<ActionResult<ActivitiesDto>> Create(ActivitiesDto dto)
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
            public async Task<ActionResult<ActivitiesDto>> Update(ActivitiesDto dto)
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
