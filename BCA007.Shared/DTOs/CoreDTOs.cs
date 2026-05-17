using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.DTOs
{
    public class LookupItemDto
    {
        public int? Id { get; set; }
        public string? Text { get; set; } = default!;
    }
    public class GenderDto
    {
        public int? Id { get; set; }
        public string? Gender_Name { get; set; } = default!;
    }
    
}
