using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.Data
{
    public class User : EntityBase
    {
        public State State { get; set; }

        // foreign key + relationships
        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }

        public virtual ICollection<UserProjectPermission> AccessibleProjects { get; set; }

        public User()
        {
            AccessibleProjects = new List<UserProjectPermission>();
        }




        #region Add/Remove methods

        public void AddAccessTo(Project project)
        {
            var permission = new UserProjectPermission { UserId = this.Id, Project = project };
            AccessibleProjects.Add(permission);
        }

        public void RemoveAccessTo(Project project)
        {
            var permission = AccessibleProjects.FirstOrDefault(o => o.Project.Equals(project));
            AccessibleProjects.Remove(permission);
        }

        public void AddAccessTo(UserProjectPermission permission)
        {
            permission.UserId = this.Id;
            AccessibleProjects.Add(permission);
        }

        public void RemoveAccessTo(UserProjectPermission permission)
        {
            AccessibleProjects.Remove(permission);
        }

        #endregion
    }
}
