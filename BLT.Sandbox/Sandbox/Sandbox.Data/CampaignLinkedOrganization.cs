using System;

namespace Sandbox.Data
{
    /// <summary>
    /// represents a possible pool of users who can be added to a particular campaign
    /// </summary>
    class CampaignLinkedGroup
    {
        public Guid CampaignId { get; set; }
        public Guid GroupId { get; set; }
        public virtual Campaign Campaign { get; set; }
        public virtual Group Group { get; set; }
    }
}
