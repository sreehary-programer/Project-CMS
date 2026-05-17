using BCA007.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Hostal
{
    public interface IHostalServices
    {
        Task<List<HostalDto>> GetAllAsync();
        Task<HostalDto> CreateAsync(HostalDto dto);
        Task<HostalDto> UpdateAsync(HostalDto dto);
        Task DeleteAsync(int id);
    }
}
