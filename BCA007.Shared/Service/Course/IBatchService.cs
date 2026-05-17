using BCA007.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Course
{
    public interface IBatchService
    {
        Task<List<BatchDto>> GetAllAsync();
        Task<BatchDto> CreateAsync(BatchDto dto);
        Task<BatchDto> UpdateAsync(BatchDto dto);
        Task DeleteAsync(int id);
    }
}
