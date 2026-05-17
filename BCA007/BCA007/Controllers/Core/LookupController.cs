using BCA007.Shared.Service.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BCA007.Controllers.Core
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly ILookupService _service;

        public LookupController(ILookupService service)
        {
            _service = service;
        }

        [HttpGet("classes")]
        public async Task<IActionResult> GetClasses()
            => Ok(await _service.GetClassesAsync());

        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses()
            => Ok(await _service.GetCoursesAsync());

        [HttpGet("batches")]
        public async Task<IActionResult> GetBatches()
            => Ok(await _service.GetBatchesAsync());

        [HttpGet("semesters")]
        public async Task<IActionResult> GetSemesters()
            => Ok(await _service.GetSemestersAsync());

        [HttpGet("divisions")]
        public async Task<IActionResult> GetDivisions()
            => Ok(await _service.GetDivisionsAsync());
        [HttpGet("genders")]
        public async Task<IActionResult> GetGenders()
            => Ok(await _service.GetGendersAsync());

        //book lookups
        [HttpGet("booktype")]
        public async Task<IActionResult> GetBookTypess()
            => Ok(await _service.GetBookTypesAsync());

        [HttpGet("bookcategory")]
        public async Task<IActionResult> GetBookCategorys()
            => Ok(await _service.GetBookCategorysAsync());

        [HttpGet("bookpublisher")]
        public async Task<IActionResult> GetBookPublishers()
            => Ok(await _service.GetBookPublishersAsync());

        [HttpGet("languages")]
        public async Task<IActionResult> GetLanguages()
            => Ok(await _service.GetLanguageAsync());
        [HttpGet("parents")]
         public async Task<IActionResult> GetParentAsync()
             => Ok(await _service.GetParentAsync());
        [HttpGet("role")]
        public async Task<IActionResult> GetRoleAsync()
             => Ok(await _service.GetRoleAsync());
        [HttpGet("activity")]
        public async Task<IActionResult> GetActivityAsync()
            => Ok(await _service.GetActivityAsync());
        [HttpGet("teachers")]
        public async Task<IActionResult> GetTeacherAsync()
            => Ok(await _service.GetTeacherAsync());
        [HttpGet("period")]
        public async Task<IActionResult> GetPeriodAsync()
           => Ok(await _service.GetPeriodAsync());
       
       

        [HttpGet("subject")]
        public async Task<IActionResult> GetSubject()
           => Ok(await _service.GetSubjetAsync());

       
        [HttpGet("session")]
        public async Task<IActionResult> GetSession()
           => Ok(await _service.GetSessionAsync());
        [HttpGet("examtype")]
        public async Task<IActionResult> GetExamType()
          => Ok(await _service.GetExamTypeAsync());
        [HttpGet("student")]
        public async Task<IActionResult> GetStudent()
         => Ok(await _service.GetStudentAsync());

        [HttpGet("allusers")]
        public async Task<IActionResult> GetUsersAsync()
            => Ok(await _service.GetUsersAsync());
        //hostal lookups
        [HttpGet("type")]
        public async Task<IActionResult> GetTypeAsync()
            => Ok(await _service.GetTypeAsync());
        [HttpGet("hostalRoom")]
        public async Task<IActionResult> GetHostalRoomAsync()
            => Ok(await _service.GetHostalRoomAsync());
        [HttpGet("hostalStudentRoom")]
        public async Task<IActionResult> GetHostalStudentRoomAsync()
            => Ok(await _service.GetHostalStudentRoomAsync());

        [HttpGet("attenddefault")]
        public async Task<IActionResult> GetAttendDefault()
         => Ok(await _service.GetAttendDefaultAsync());

    }
}
