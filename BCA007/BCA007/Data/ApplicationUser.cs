using Microsoft.AspNetCore.Identity;

namespace BCA007.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<int>
    {
        public string? FullName { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        public int? Class_Id { get; set; }
        public int? Gender_Id { get; set; }
        public int? Parent_Id { get; set; }

        public int? BusRoute_Id { get; set; }
        public int? HostelRoom_Id { get; set; }
        public DateTime? NextPaymentDueDate { get; set; }
        public string? CurrentPaymentStatus { get; set; }
        public string? ProfileURL { get; set; }
    }
    public class ApplicationRole : IdentityRole<int> { }

}
