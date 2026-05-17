using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.DTOs
{
    public class HostalDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hostal Name is required")]
        [StringLength(50,
        MinimumLength = 2,
        ErrorMessage = "Hostal Name must be between 2 and 50 characters")]
        public string Hostel_Name { get; set; } = string.Empty;

        [Required]
        public int? Type_Id { get; set; }

        [Required(ErrorMessage = "Warden Name is required")]
        [StringLength(50, ErrorMessage = "Warden Name max 50 characters")]
        public string Warden_Name { get; set; } = string.Empty;
    }
    public class HostelTypeDto
    {
        public int? Id { get; set; }
        public string? Type { get; set; } = default!;
    }
    public class HostalRoomDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Room Number is required")]
        [StringLength(50,
        MinimumLength = 1,
        ErrorMessage = "Room Number  must be between 1 and 50 characters")]
        public string? Room_Number { get; set; } = string.Empty;

        [Required]
        public int? Hostel_Id { get; set; }

        [Required(ErrorMessage = "Capacity of the Room Is Requirred")]
        public int? Capacity { get; set; }

    }
  
    public class HostalViewDto
    {
        public int Id { get; set; }
        public string? Hostel_FullName { get; set; } = default!;
        public string? UserName { get; set; } = default!;
        public string? FullName { get; set; } = default!;
        public int? Capacity { get; set; }
        public int? Hostel_Id { get; set; }
        public string? Room_Number { get; set; } = default!;
        public int? HostelRoom_Id { get; set; }


    }
    public class HostalOccupiedViewDto
    {
        public int Id { get; set; }
        public string Hostel_FullName { get; set; } = default!;
        public int? Occupied { get; set; }
        public int? Capacity { get; set; }
        public string Room_Number { get; set; } = default!;
        public string Hostel_Name { get; set; } = default!;
        public int? Hostel_Id { get; set; }
        public int? Hostelp_Id { get; set; }





    }
}
