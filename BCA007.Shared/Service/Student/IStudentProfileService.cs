using BCA007.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Student
{
    public interface IStudentProfileService
    {
        Task<List<StudentViewProfileDto>> GetAllAsync();
        Task<List<HostelViewDto>> GetAllHostelAsync(int stdId);
        Task<List<TimetablesViewDto>> GetAllTimetableAsync(int stdId);
        Task<List<BusViewDto>> GetAllBusAsync(int stdId);
        Task<List<BookHistoryViewDto>> GetAllBookHistoryAsync(int stdId);
        Task<List<AttendanceViewDto>> GetAllAttendanceAsync(int stdId);
        Task<List<AttendancePersentageViewDto>> GetAllPersentageAsync(int stdId);
        Task<List<ResultsViewDto>> GetAllResultAsync(int stdId);
        Task<List<FeeDto>> GetAllFeeAsync(int stdId);



    }
}
