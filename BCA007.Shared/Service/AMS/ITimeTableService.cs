using BCA007.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.AMS
{
    public interface ITimeTableService
    {
        Task<List<TimetableViewDto>> GetAllAsync();
        Task<TimetableDto> CreateAsync(TimetableDto dto);
        Task<TimetableDto> UpdateAsync(TimetableDto dto);
        Task DeleteAsync(int id);
    }
}
