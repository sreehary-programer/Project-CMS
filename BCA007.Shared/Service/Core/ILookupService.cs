using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.AMS;

namespace BCA007.Shared.Service.Core
{
    public interface ILookupService
    {
        //Course service lookups    
        Task<List<LookupItemDto>> GetClassesAsync();
        Task<List<LookupItemDto>> GetCoursesAsync();
        Task<List<LookupItemDto>> GetBatchesAsync();
        Task<List<LookupItemDto>> GetSemestersAsync();
        Task<List<LookupItemDto>> GetDivisionsAsync();
        Task<List<LookupItemDto>> GetGendersAsync();

        //Book service lookups
        Task<List<LookupItemDto>> GetBookTypesAsync();
        Task<List<LookupItemDto>> GetBookPublishersAsync();
        Task<List<LookupItemDto>> GetBookCategorysAsync();
        Task<List<LookupItemDto>> GetLanguageAsync();
        //Parent service lookups
        Task<List<LookupItemDto>> GetParentAsync();

        //role
        Task<List<LookupItemDto>> GetRoleAsync();
        Task<List<LookupItemDto>> GetUsersAsync();
        //AMS

        Task<List<LookupItemDto>> GetActivityAsync();
        Task<List<LookupItemDto>> GetTeacherAsync();
        Task<List<LookupItemDto>> GetPeriodAsync();
        Task<List<LookupItemDto>> GetSubjetAsync();
        Task<List<LookupItemDto>> GetSessionAsync();
        Task<List<LookupItemDto>> GetExamTypeAsync();
        Task<List<LookupItemDto>> GetStudentAsync();


        //Hostel
        Task<List<LookupItemDto>> GetHostalRoomAsync();
        Task<List<LookupItemDto>> GetHostalStudentRoomAsync();
        Task<List<LookupItemDto>> GetTypeAsync();

        Task<List<LookupItemDto>> GetAttendDefaultAsync();


    }
}
