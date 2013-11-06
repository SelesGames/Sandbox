using System;
using System.Collections.Generic;

namespace Sandbox.Data
{
    public class Category //: EntityBase
    {
        // primary key
        public Guid Id { get; set; }

        public string Name { get; set; }
        public int ProjectCount { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public Category()
        {
            Projects = new List<Project>();
        }
    }
}
