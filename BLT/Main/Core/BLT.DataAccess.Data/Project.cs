using System;
using System.Collections.Generic;
using System.Linq;

namespace BLT.ClientExtranet.Data.Models
{
    public class Project : EntityBase
    {
        public State State { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        // properties updated via triggers (whenever projects are added/edited/removed)
        public DateTime? LatestRoundModified { get; set; }

        // foreign key + relationships
        public Guid GroupId { get; set; }
        public Guid CampaignId { get; set; }
        public virtual Group Group { get; set; }
        public virtual Campaign Campaign { get; set; }

        public virtual ICollection<Round> Rounds { get; set; }
        public virtual ICollection<UserProjectPermission> UsersWithAccess { get; set; }

        public Project()
        {
            Rounds = new List<Round>();
            UsersWithAccess = new List<UserProjectPermission>();
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

        public void AddAccessFor(User user)
        {
            var permission = new UserProjectPermission { User = user, Project = this };
            UsersWithAccess.Add(permission);
        }

        public void RemoveAccessFor(User user)
        {
            var permission = UsersWithAccess.FirstOrDefault(o => o.User.Equals(user));
            UsersWithAccess.Remove(permission);
        }

        public void AddAccessFor(UserProjectPermission permission)
        {
            permission.Project = this;
            UsersWithAccess.Add(permission);
        }

        public void RemoveAccessFor(UserProjectPermission permission)
        {
            UsersWithAccess.Remove(permission);
        }

        #endregion
    }
}
