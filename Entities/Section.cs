using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class Section
    {
        public Section()
        {
            Teams = new HashSet<Team>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
