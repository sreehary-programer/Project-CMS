using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCA007.Shared.DTOs
{
    [Table("T_Staff_Attendance")] // Explicitly map if needed, though Context does it too
    public class StaffAttendanceDto
    {
        [Key]
        public int Id { get; set; }

        public int StaffId { get; set; }

        public DateOnly Date { get; set; }

        public int StatusId { get; set; }

        [StringLength(500)]
        public string? Remarks { get; set; }

        public DateTime CreatedAt { get; set; }

        public TimeOnly CheckInTime { get; set; }

        public TimeOnly CheckOutTime { get; set; }
    }
}
