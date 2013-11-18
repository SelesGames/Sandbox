using Microsoft.AspNet.Identity.EntityFramework;
using PlatformUsers.Identity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformUsers.Identity.DataContext
{
    internal class PlatformUserIdentityDbContext : IdentityDbContext<PlatformUser>, IDisposable
    {
        public PlatformUserIdentityDbContext()
            : base("[YourConnectionString]")

        {

            #if DEBUG

                //Write SQL out to output log
                Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            #endif

            // DISABLE MODEL SCHEMA MIGRATIONS
            // Database.SetInitializer<ClientIdentityDbContext>(null);

            // Destroy Database When Model Changes:
                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PlatformUserIdentityDbContext>());


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
                .ToTable("PlatformUsers", "dbo"); //.Property(p => p.Id).HasColumnName("User_Id");
            modelBuilder.Entity<PlatformUser>()
                .ToTable("PlatformUsers", "dbo"); //.Property(p => p.Id).HasColumnName("User_Id");


            modelBuilder.Entity<IdentityRole>()
                .ToTable("PlatformUserRoles", "dbo");

            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("PlatformUserClaims", "dbo");

            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("PlatformUserLogins", "dbo");

            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("PlatformUsersInRoles", "dbo");

        }
    }
}
