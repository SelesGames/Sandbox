﻿using System;
using System.Collections.Generic;

namespace Sandbox.Data
{
    public class Group : EntityBase
    {
        public State State { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }

        // properties updated via triggers (whenever projects are added/edited/removed)
        public int ProjectCount { get; set; }
        public DateTime? LatestProjectTime { get; set; }
        public string LatestProjectName { get; set; }

        // foreign key + relationships
        public Guid? LatestProjectId { get; set; }
        public virtual Project LatestProject { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual IReadOnlyCollection<Project> Projects { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public Group()
        {
            Campaigns = new List<Campaign>();
            Projects = new List<Project>();
            Users = new List<User>();
        }




        #region Add/Remove methods

        public void Add(Campaign campaign)
        {
            campaign.Group = this;
            Campaigns.Add(campaign);
        }

        public void Remove(Campaign campaign)
        {
            Campaigns.Remove(campaign);
        }

        public void Add(User user)
        {
            user.Group = this;
            Users.Add(user);
        }

        public void Remove(User user)
        {
            Users.Remove(user);
        }

        #endregion
    }
}