using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Bus
{
    public interface IBusService
    {
        Task<List<BusViewsDto>> GetAllAsync();
        //Task CreateAsync(StudentDto dto);
        Task<BusDto> CreateAsync(BusDto dto);
        Task<BusDto> UpdateAsync(BusDto dto);

        Task DeleteAsync(int id);
        Task<List<DriverViewDto>> GetDriversAsync();
    }
}
