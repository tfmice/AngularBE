using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class Team
    {
        public Team()
        {
            TeamMembers = new HashSet<TeamMember>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? SectionId { get; set; }

        public virtual Section? Section { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
    }
}
