using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class Employee
    {
        public Employee()
        {
            TeamMembers = new HashSet<TeamMember>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateOnly? Birthdate { get; set; }
        public string? Hobby { get; set; }
        public string? Quote { get; set; }
        public string? Opinion { get; set; }
        public string? Role { get; set; }

        public virtual ICollection<TeamMember> TeamMembers { get; set; }
    }
}
