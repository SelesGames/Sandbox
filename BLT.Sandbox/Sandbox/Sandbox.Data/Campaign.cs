using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.Data
{
    public class Campaign : EntityBase
    {
        public string Name { get; set; }
        public int ProjectCount { get; set; }
        public DateTime? LatestProjectTime { get; set; }
        public string LatestProjectName { get; set; }

        // foreign key + relationships
        public Guid ClientId { get; set; }
        //public Guid? LatestProjectId { get; set; }
        public virtual Client Client { get; set; }
        //public virtual Project LatestProject { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<UserCampaignPermission> UsersWithAccess { get; set; }

        public Campaign()
        {
            Projects = new List<Project>();
            UsersWithAccess = new List<UserCampaignPermission>();
        }




        #region Add/Remove methods

        public void Add(Project project)
        {
            project.Client = Client;
            project.Campaign = this;
            Projects.Add(project);
        }

        public void Remove(Project project)
        {
            Projects.Remove(project);
        }

        public void AddAccessFor(User user)
        {
            var permission = new UserCampaignPermission { User = user, Campaign = this };
            UsersWithAccess.Add(permission);
        }

        public void RemoveAccessFor(User user)
        {
            var permission = UsersWithAccess.FirstOrDefault(o => o.User.Equals(user));
            UsersWithAccess.Remove(permission);
        }

        public void AddAccessFor(UserCampaignPermission permission)
        {
            permission.Campaign = this;
            UsersWithAccess.Add(permission);
        }

        public void RemoveAccessFor(UserCampaignPermission permission)
        {
            UsersWithAccess.Remove(permission);
        }

        #endregion
    }
}
