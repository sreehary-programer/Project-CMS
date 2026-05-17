using BCA007.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Course
{
    public interface ISemesterService
    {
        Task<List<SemesterDto>> GetAllAsync();
        Task<SemesterDto> CreateAsync(SemesterDto dto);
        Task<SemesterDto> UpdateAsync(SemesterDto dto);
        Task DeleteAsync(int id);
    }
}
