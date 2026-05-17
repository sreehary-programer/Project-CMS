using BCA007.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Course
{
    public interface IDivisionService
    {
        Task<List<DivisionDto>> GetAllAsync();
        Task<DivisionDto> CreateAsync(DivisionDto dto);
        Task<DivisionDto> UpdateAsync(DivisionDto dto);
        Task DeleteAsync(int id);
    }
}
