using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCA007.Shared.DTOs
{
    public class StaffAttendanceViewDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int StaffId { get; set; }

        public string? FullName { get; set; }

        public DateOnly Date { get; set; }

        public int StatusId { get; set; }

        public string Status { get; set; } = null!;

        [StringLength(500)]
        public string? Remarks { get; set; }

        public DateTime CreatedAt { get; set; }

        public TimeOnly CheckInTime { get; set; }

        public TimeOnly CheckOutTime { get; set; }
    }
}
