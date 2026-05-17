using BCA007.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Attendance
{
    public interface IAttendEntryService
    {
        Task<List<AttendEntryViewDto>> GetAllAsync();
        Task<AttendEntryDto> CreateAsync(AttendEntryDto dto);
        Task<AttendEntryDto> UpdateAsync(AttendEntryDto dto);
        Task<List<AttendEntryViewDto>> GetAttendanceById(int? Id);
        Task DeleteAsync(int id);
    }
}
