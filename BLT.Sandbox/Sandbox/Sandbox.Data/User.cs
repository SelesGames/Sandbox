using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.Data
{
    public class User : EntityBase
    {
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }

        public virtual ICollection<UserCampaignPermission> AccessibleCampaigns { get; set; }

        public User()
        {
            AccessibleCampaigns = new List<UserCampaignPermission>();
        }




        #region Add/Remove methods

        public void AddAccessTo(Campaign campaign)
        {
            var permission = new UserCampaignPermission { User = this, Campaign = campaign };
            AccessibleCampaigns.Add(permission);
        }

        public void RemoveAccessTo(Campaign campaign)
        {
            var permission = AccessibleCampaigns.FirstOrDefault(o => o.Campaign.Equals(campaign));
            AccessibleCampaigns.Remove(permission);
        }

        public void AddAccessTo(UserCampaignPermission permission)
        {
            permission.User = this;
            AccessibleCampaigns.Add(permission);
        }

        public void RemoveAccessTo(UserCampaignPermission permission)
        {
            AccessibleCampaigns.Remove(permission);
        }

        #endregion
    }
}
