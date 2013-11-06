using System;
using System.Collections.Generic;

namespace Sandbox.Data
{
    public class Campaign
    {
        // surrogate and primary key
        //public int Id { get; set; }
        public Guid Id { get; set; }

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
        public virtual ICollection<User> Users { get; set; }

        public Campaign()
        {
            Projects = new List<Project>();
            Users = new List<User>();
        }
    }
}
