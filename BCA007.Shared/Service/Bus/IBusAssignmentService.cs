using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Bus
{
    public interface IBusAssignmentService
    {
        Task<List<BusAssignmentDto>> GetAllAsync();
        //Task CreateAsync(StudentDto dto);
        Task<BusAssignmentDto> CreateAsync(BusAssignmentDto dto);
        Task<BusAssignmentDto> UpdateAsync(BusAssignmentDto dto);
        Task DeleteAsync(int id);
    }
}
