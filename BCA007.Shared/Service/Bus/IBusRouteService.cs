using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Bus
{
    public interface IBusRouteService
    {
        Task<List<BusRouteDto>> GetAllAsync();
        //Task CreateAsync(StudentDto dto);
        Task<BusRouteDto> CreateAsync( BusRouteDto dto);
        Task<BusRouteDto> UpdateAsync( BusRouteDto dto);
        Task DeleteAsync(int id);
    }
}
