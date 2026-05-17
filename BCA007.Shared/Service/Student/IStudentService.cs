using BCA007.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Student
{
    public interface IStudentService
    {
        Task<List<StudentViewDto>> GetAllAsync();
        //Task CreateAsync(StudentDto dto);
        Task<StudentDto> CreateAsync(StudentDto dto, Stream? fileStream, string? fileName);
        Task<StudentDto> UpdateAsync(StudentDto dto, Stream? fileStream, string? fileName);
        Task DeleteAsync(int id);
    }
}
