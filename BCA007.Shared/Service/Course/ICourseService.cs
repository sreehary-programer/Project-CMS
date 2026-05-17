using BCA007.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Course
{
    public interface ICourseService
    {
        Task<List<CourseDto>> GetAllAsync();
        Task<CourseDto> CreateAsync(CourseDto dto);
        Task<CourseDto> UpdateAsync(CourseDto dto);
        Task DeleteAsync(int id);
    }
}
