using System;
using System.Collections.Generic;

namespace Sandbox.Data
{
    public class User : EntityBase
    {
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }

        public virtual ICollection<Campaign> AccessibleCampaigns { get; set; }

        public User()
        {
            AccessibleCampaigns = new List<Campaign>();
        }




        #region Add/Remove methods

        public void AddAccessTo(Campaign campaign)
        {
            AccessibleCampaigns.Add(campaign);
        }

        public void RemoveAccessTo(Campaign campaign)
        {
            AccessibleCampaigns.Remove(campaign);
        }

        #endregion
    }
}
