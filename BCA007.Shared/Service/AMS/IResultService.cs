using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCA007.Shared.DTOs.AMS;

namespace BCA007.Shared.Service.AMS
{
    public interface IResultService
    {
        Task<List<ResultViewDto>> GetAllAsync();
        Task UpdateResults(List<ResultViewDto> dto);

        Task<List<ResultViewDto>> GetResultsByExamId(int? examId);
        Task DeleteAsync(int id);


    }
}
