using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class TeamMember
    {
        public int Id { get; set; }
        public int? TeamId { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual Team? Team { get; set; }
    }
}
