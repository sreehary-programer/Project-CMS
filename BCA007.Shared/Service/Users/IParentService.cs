using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCA007.Shared.DTOs;

namespace BCA007.Shared.Service.Users
{
    public interface IParentService
    {

        Task<List<ParentViewDto>> GetAllAsync();
        //Task CreateAsync(StudentDto dto);
        Task<ParentDto> CreateAsync(ParentDto dto, Stream? fileStream, string? fileName);
        Task<ParentDto> UpdateAsync(ParentDto dto, Stream? fileStream, string? fileName);
        Task DeleteAsync(int id);
    }
}
