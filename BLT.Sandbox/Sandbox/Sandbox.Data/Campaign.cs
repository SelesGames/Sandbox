using System;
using System.Collections.Generic;

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
        public Guid? LatestProjectId { get; set; }
        public virtual Client Client { get; set; }
        public virtual Project LatestProject { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<User> UsersWithAccess { get; set; }

        public Campaign()
        {
            Projects = new List<Project>();
            UsersWithAccess = new List<User>();
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
            UsersWithAccess.Add(user);
        }

        public void RemoveAccessFor(User user)
        {
            UsersWithAccess.Remove(user);
        }

        #endregion
    }
}
