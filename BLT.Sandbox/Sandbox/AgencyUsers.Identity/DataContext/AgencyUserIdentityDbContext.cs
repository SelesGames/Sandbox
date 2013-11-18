using AgencyUsers.Identity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyUsers.Identity.DataContext
{
    internal class AgencyUserIdentityDbContext : IdentityDbContext<AgencyUser>, IDisposable
    {
        public AgencyUserIdentityDbContext()
            : base("[YourConnectionString]")

        {

            #if DEBUG

                //Write SQL out to output log
                Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            #endif

            // DISABLE MODEL SCHEMA MIGRATIONS
            // Database.SetInitializer<ClientIdentityDbContext>(null);

            // Destroy Database When Model Changes:
                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AgencyUserIdentityDbContext>());


            // REQUIRED FOR OPTIMAL QUERIES WHEN SELECTING PRIMARY KEYS
            // outputs "where userId= 'blah" instead of
            // "where userId = 'blah' and userId is not null"
            Configuration.UseDatabaseNullSemantics = true;

        }

        //public override DbSet<ClientUser> Users { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<IdentityUser>()
                .ToTable("AgencyUsers", "dbo"); //.Property(p => p.Id).HasColumnName("User_Id");
            modelBuilder.Entity<AgencyUser>()
                .ToTable("AgencyUsers", "dbo"); //.Property(p => p.Id).HasColumnName("User_Id");


            modelBuilder.Entity<IdentityRole>()
                .ToTable("AgencyUserRoles", "dbo");

            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("AgencyUserClaims", "dbo");

            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("AgencyUserLogins", "dbo");

            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("AgencyUsersInRoles", "dbo");

        }
    }
}
