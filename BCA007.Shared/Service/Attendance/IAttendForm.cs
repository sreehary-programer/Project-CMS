using BCA007.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Attendance
{
    public interface IAttendFormService
    {
        Task<List<AttendFormViewDto>> GetAllAsync();
        //Task<AttendFormDto> CreateAsync(AttendFormDto dto);
        Task<AttendFormDto> UpdateAsync(AttendFormDto dto);
        Task DeleteAsync(int id);
    }
}
