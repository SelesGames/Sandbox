using System;
using System.Collections.Generic;

namespace Sandbox.Data
{
    public class User : EntityBase
    {
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }

        public User()
        {
            Clients = new List<Client>();
            Campaigns = new List<Campaign>();
        }
    }
}
