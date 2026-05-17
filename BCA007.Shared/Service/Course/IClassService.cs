using BCA007.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Course
{
    public interface IClassService
    {
        Task<List<ClassViewDto>> GetAllAsync();
        Task<ClassDto> CreateAsync(ClassDto dto);
        Task<ClassDto> UpdateAsync(ClassDto dto);
        Task DeleteAsync(int id);
    }
}
