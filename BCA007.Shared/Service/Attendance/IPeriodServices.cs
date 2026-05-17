using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.AMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Attendance
{
    public interface IPeriodService
    {
        Task<List<PeriodDto>> GetAllAsync();
        Task<PeriodDto> CreateAsync(PeriodDto dto);
        Task<PeriodDto> UpdateAsync(PeriodDto dto);
        Task DeleteAsync(int id);
    }
}
