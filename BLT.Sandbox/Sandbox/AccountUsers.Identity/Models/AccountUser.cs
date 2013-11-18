using Microsoft.AspNet.Identity.EntityFramework;
using Sandbox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountUsers.Identity.Models
{
    public class AccountUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }

        public Guid ClientId { get; set; }
        public virtual Organization Client { get; set; }
    }
}
