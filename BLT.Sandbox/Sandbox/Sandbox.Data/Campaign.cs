﻿using System;
using System.Collections.Generic;

namespace Sandbox.Data
{
    public class Campaign : EntityBase
    {
        public State State { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        // properties updated via triggers (whenever projects are added/edited/removed)
        public int ProjectCount { get; set; }
        public DateTime? LatestProjectTime { get; set; }
        public string LatestProjectName { get; set; }

        // foreign key + relationships
        public Guid GroupId { get; set; }
        public Guid? LatestProjectId { get; set; }
        public virtual Group Group { get; set; }
        public virtual Project LatestProject { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public Campaign()
        {
            Projects = new List<Project>();
        }




        #region Add/Remove methods

        public void Add(Project project)
        {
            project.Group = Group;
            project.Campaign = this;
            Projects.Add(project);
        }

        public void Remove(Project project)
        {
            Projects.Remove(project);
        }

        public Project AddProject(string name, string imageUrl = null, State state = State.Active)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name");

            var project = new Project
            {
                Name = name,
                ImageUrl = imageUrl,
                State = state,
            };
            Add(project);
            return project;
        }

        #endregion
    }
}
