using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.Library;
using BCA007.Shared.Service.Bus;
using BCA007.Shared.Service.Course;
using BCA007.Shared.Service.Library;
using BCA007.Shared.Service.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BCA007.Controllers.Bus
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Teacher,Librarian,Accountant,Principal")]
    public class BusRouteController : ControllerBase
    {
        private readonly IBusRouteService _serv;

        public BusRouteController(IBusRouteService serv)
        {
            _serv = serv;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _serv.GetAllAsync());
        }
        [HttpPost("create")]
        public async Task<ActionResult<BusRouteDto>> Create([FromBody] BusRouteDto dto)
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("edit")]
        public async Task<ActionResult<BusRouteDto>> Update([FromBody] BusRouteDto dto)
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
