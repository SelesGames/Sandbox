using System;

namespace Sandbox.Data
{
    public class UserCampaignPermission
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CampaignId { get; set; }

        public virtual User User { get; set; }
        public virtual Campaign Campaign { get; set; }
    }
}
