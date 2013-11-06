using System;
using System.Collections.Generic;

namespace Sandbox.Data
{
    public class User
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

        public User()
        {
            Clients = new List<Client>();
            Projects = new List<Project>();
        }
    }
}
