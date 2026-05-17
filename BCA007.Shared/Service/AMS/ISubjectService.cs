using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.AMS;

namespace BCA007.Shared.Service.AMS
{
    public interface ISubjectService
    {
        Task<List<SubjectDto>> GetAllAsync();
        Task<SubjectDto> CreateAsync(SubjectDto dto);
        Task<SubjectDto> UpdateAsync(SubjectDto dto);
        Task DeleteAsync(int id);
    }
}
