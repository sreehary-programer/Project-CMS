using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCA007.Shared.DTOs.AMS;

namespace BCA007.Shared.Service.AMS
{
    public interface IActivitiesService
    {
        Task<List<ActivitiesDto>> GetAllAsync();
        Task<ActivitiesDto> CreateAsync(ActivitiesDto dto);
        Task<ActivitiesDto> UpdateAsync(ActivitiesDto dto);
        Task DeleteAsync(int id);
    }
}
