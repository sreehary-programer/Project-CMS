using BCA007.Services.Student;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BCA007.Controllers.Student
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

  
        [HttpPost("create")]
        public async Task<ActionResult<StudentDto>> Create([FromForm] string dto, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var model = JsonSerializer.Deserialize<StudentDto>(dto)!;

                return Ok(await _service.CreateAsync(model,file?.OpenReadStream(), file?.FileName));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("edit")]
        public async Task<ActionResult<StudentDto>> Update([FromForm] string dto, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var model = JsonSerializer.Deserialize<StudentDto>(dto)!;
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
      //[HttpPost("createx")]
        //public async Task<IActionResult> Create(StudentDto dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        await _service.CreateAsync(dto);
        //        return Ok();
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return Conflict(ex.Message);
        //    }
        //}
  //[HttpPut("edit")]
        //public async Task<ActionResult<DivisionDto>> Update(StudentDto dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    try
        //    {
        //        await _service.UpdateAsync(dto);
        //        return Ok();
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }

        //    catch (InvalidOperationException ex)
        //    {
        //        return Conflict(ex.Message); // 409
        //    }
        //}