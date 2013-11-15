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

        public virtual ICollection<Round> Rounds { get; set; }

        public Project()
        {
            Rounds = new List<Round>();
        }




        #region Add/Remove methods

        public void Add(Round round)
        {
            round.Project = this;
            Rounds.Add(round);
        }

        public void Remove(Round round)
        {
            Rounds.Remove(round);
        }

        #endregion
    }
}
