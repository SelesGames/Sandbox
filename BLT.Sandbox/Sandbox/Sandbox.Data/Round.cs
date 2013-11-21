using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.Data
{
    public class Round : EntityBase
    {
        public RoundState State { get; set; } // Draft, InReview, Published, Archived
        public DateTime CreatedOn { get; set; }
        public string RoundNumber { get; set; } // maybe call round label

        // properties updated via triggers (whenever projects are added/edited/removed)
        public int ContentCount { get; set; }

        // foreign key + relationships
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public virtual ICollection<Content> Contents { get; set; }
        public virtual ICollection<RoundApproval> Approvals { get; set; }

        public Round()
        {
            Contents = new List<Content>();
            Approvals = new List<RoundApproval>();
        }




        #region Add/Remove methods

        public void Add(Content content)
        {
            content.Round = this;
            Contents.Add(content);
        }

        public void Remove(Content content)
        {
            Contents.Remove(content);
        }

        public void AddApproval(User user, bool gaveApproval)
        {
            var approval = new RoundApproval { Round = this, User = user, GaveApproval = gaveApproval };
            Approvals.Add(approval);
        }

        public void RemoveApproval(User user)
        {
            var approval = Approvals.FirstOrDefault(o => o.User.Equals(user));
            Approvals.Remove(approval);
        }

        public void AddApproval(RoundApproval approval)
        {
            approval.Round = this;
            Approvals.Add(approval);
        }

        public void RemoveApproval(RoundApproval approval)
        {
            Approvals.Remove(approval);
        }

        #endregion
    }
}
