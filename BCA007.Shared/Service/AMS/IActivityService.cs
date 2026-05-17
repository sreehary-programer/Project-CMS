using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCA007.Shared.DTOs.AMS;

namespace BCA007.Shared.Service.AMS
{
    public interface IActivityService
    {
        Task<List<ActivityViewDto>> GetAllAsync();
        Task<ActivityDto> CreateAsync(ActivityDto dto);
        Task<ActivityDto> UpdateAsync(ActivityDto dto);
        Task DeleteAsync(int id);
    }
}
