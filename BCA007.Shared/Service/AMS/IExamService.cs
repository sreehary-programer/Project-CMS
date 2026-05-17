using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCA007.Shared.DTOs.AMS;

namespace BCA007.Shared.Service.AMS
{
    public interface IExamService
    {
        Task<List<ExamTimeTableViewDto>> GetAllAsync();
        Task<ExamTimeTableDto> UpdateAsync(ExamTimeTableDto dto);
        Task DeleteAsync(int id);
    }
}
