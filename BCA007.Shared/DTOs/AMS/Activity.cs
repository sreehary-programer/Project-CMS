using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.DTOs.AMS
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public int? ActivityId { get; set; }
        [NotMapped]

        public string ActivityName { get; set; } = string.Empty;
        [NotMapped]

        public string? Description { get; set; }

        public int? ClassId { get; set; }
        public DateTime? PlannedDate { get; set; }
        public DateTime? ConductedDate { get; set; }
        [NotMapped]

        public string IsConducted { get; set; } = string.Empty;
        public int? PeriodId { get; set; }

        public int? PlannedTeacherId { get; set; }
        public int? ConductedTeacherId { get; set; }


        public string? Feedback { get; set; }
    }

    public class ActivityViewDto
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; } = string.Empty;
        public int? PeriodId { get; set; }
        public string? Period_Name { get; set; } = string.Empty;

        public DateTime? PlannedDate { get; set; }
        public DateTime? ConductedDate { get; set; }
        public string? Description { get; set; }

        public string IsConducted { get; set; } = string.Empty;

        public string? Feedback { get; set; }

        /* Class Info */
        public int? ClassId { get; set; }
        public string Class_Name { get; set; } = string.Empty;

        /* Planned Teacher */
        public int? PlannedTeacherId { get; set; }
        public string PlannedTeacherName { get; set; } = string.Empty;
        public string PlannedTeacherEmail { get; set; } = string.Empty;
        public bool IsPlannedTeacherLocked { get; set; }

        /* Conducted Teacher */
        public int? ConductedTeacherId { get; set; }
        public string? ConductedTeacherName { get; set; }
    }
    public class ActivitiesDto
    {
        public int Id { get; set; }

        public string ActivityName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
    public class TeacherDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
    public class PeriodDto
    {
        public int Id { get; set; }
        public string? Period_Name { get; set; } = string.Empty;

    }
}
