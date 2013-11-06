using System;
using System.Collections.Generic;

namespace Sandbox.Data
{
    public class Project //: EntityBase
    {
        // primary key
        public Guid Id { get; set; }

        public string Name { get; set; }
        public DateTime? LatestRoundModified { get; set; }

        // foreign key + relationships
        public Guid ClientId { get; set; }
        public Guid CampaignId { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Client Client { get; set; }
        public virtual Campaign Campaign { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Round> Rounds { get; set; }

        public Project()
        {
            Rounds = new List<Round>();
        }
    }
}
