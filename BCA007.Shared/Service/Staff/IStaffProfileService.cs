using BCA007.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Staff
{
    public interface IStaffProfileService
    {
        Task<List<StaffViewDto>> GetAllAsync();


    }
}
