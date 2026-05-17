using BCA007.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Hostal
{
    public interface IHostaltService
    {
        Task<List<HostalViewDto>> GetAllAsync();
        //Task<HostalViewDto> GetByIdAsync(int StudentId);
        //Task CreateAsync(AdminDto dto);
        Task<HostalRoomDto> CreateAsync(HostalRoomDto dto);
        Task<HostalRoomDto> UpdateAsync(HostalRoomDto dto);
        Task DeleteAsync(int id);
        Task DeallocateAsync(string userName);
    }
}
