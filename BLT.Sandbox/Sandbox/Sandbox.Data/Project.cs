using System;
using System.Collections.Generic;

namespace Sandbox.Data
{
    public class Project : EntityBase
    {
        public string Name { get; set; }
        public string CoverImageUrl { get; set; }
        public DateTime? LatestRoundModified { get; set; }

        // foreign key + relationships
        public Guid ClientId { get; set; }
        public Guid CampaignId { get; set; }
        public virtual Client Client { get; set; }
        public virtual Campaign Campaign { get; set; }

        string coverarturl;

        public virtual ICollection<Round> Rounds { get; set; }

        public Project()
        {
            Rounds = new List<Round>();
        }
    }
}
