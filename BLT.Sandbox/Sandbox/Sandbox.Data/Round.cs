using System;
using System.Collections.Generic;

namespace Sandbox.Data
{
    public class Round
    {
        // surrogate and primary key
        public int Id { get; set; }
        public Guid RoundId { get; set; }

        public int FileCount { get; set; }
        public DateTime CreatedOn { get; set; }

        // foreign key + relationships
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public virtual ICollection<Content> Contents { get; set; }

        public Round()
        {
            Contents = new List<Content>();
        }
    }
}
