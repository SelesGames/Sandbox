using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace Sandbox.Data.Entity
{
    public class DataContext : DbContext
    {
        #region Constructors

        public DataContext() : base(Connection.CONNECTION_STRING)
        {
            Debug.Write(Connection.CONNECTION_STRING);
            Initialize();
        }

        public DataContext(string connectionString)
            : base(connectionString)
        {
            Initialize();
        }

        void Initialize()
        {
#if DEBUG
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
#endif
            // DISABLE MODEL SCHEMA MIGRATIONS
            //Database.SetInitializer<DataContext>(null);
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());

            // REQUIRED FOR OPTIMAL QUERIES WHEN SELECTING PRIMARY KEYS
            // outputs "where userId = 'blah'" instead of
            // "where userId = 'blah' and userId is not null"
            Configuration.UseDatabaseNullSemantics = true;
        }

        #endregion




        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Content> Contents { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // removes the default option of pluralizing table names: i.e. if the class is 'Campaign' it tries to name the SQL table to 'Campaigns'
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Turn off Cascade-on-Delete
            modelBuilder.Entity<Project>().HasRequired(o => o.Group).WithMany(o => (ICollection<Project>)o.Projects).HasForeignKey(o => o.GroupId).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasRequired(o => o.Group).WithMany(o => o.Users).HasForeignKey(o => o.GroupId).WillCascadeOnDelete(false);
            
            // Specify OPTIONAL foreign key mappings
            modelBuilder.Entity<Group>().HasOptional(o => o.LatestProject).WithMany().HasForeignKey(o => o.LatestProjectId);
            modelBuilder.Entity<Campaign>().HasOptional(o => o.LatestProject).WithMany().HasForeignKey(o => o.LatestProjectId);

            // specify that the RowVersion property should be configured as a "rowversion" SQL DATA type
            modelBuilder.Properties().Where(o => o.Name.Equals("RowVersion"))
                .Configure(config => config.IsRowVersion());

            base.OnModelCreating(modelBuilder);
        }




        #region Unused mappings/conventions

        //void RemoveForeignKeyConventions(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<AssociationInverseDiscoveryConvention>();
        //    modelBuilder.Conventions.Remove<ForeignKeyDiscoveryConvention>();
        //    modelBuilder.Conventions.Remove<ForeignKeyAssociationMultiplicityConvention>();
        //    modelBuilder.Conventions.Remove<ForeignKeyNavigationPropertyAttributeConvention>();
        //    modelBuilder.Conventions.Remove<ForeignKeyPrimitivePropertyAttributeConvention>();
        //    modelBuilder.Conventions.Remove<NavigationPropertyNameForeignKeyDiscoveryConvention>();
        //    modelBuilder.Conventions.Remove<PrimaryKeyNameForeignKeyDiscoveryConvention>();
        //    modelBuilder.Conventions.Remove<TypeNameForeignKeyDiscoveryConvention>();
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //}

        //void ExplicitlyMapForeignKeys(DbModelBuilder modelBuilder)
        //{
        //    // Campaign foreign key mappings
        //    modelBuilder.Entity<Campaign>().HasRequired(o => o.Group).WithMany(o => o.Campaigns).HasForeignKey(o => o.GroupId);

        //    // Project foreign key mappings
        //    modelBuilder.Entity<Project>().HasRequired(o => o.Group).WithMany(o => (ICollection<Project>)o.Projects).HasForeignKey(o => o.GroupId).WillCascadeOnDelete(false);
        //    modelBuilder.Entity<Project>().HasRequired(o => o.Campaign).WithMany(o => o.Projects).HasForeignKey(o => o.CampaignId);

        //    // Round foreign key mappings
        //    modelBuilder.Entity<Round>().HasRequired(o => o.Project).WithMany(o => o.Rounds).HasForeignKey(o => o.ProjectId);

        //    // Content foreign key mappings
        //    modelBuilder.Entity<Content>().HasRequired(o => o.Round).WithMany(o => o.Contents).HasForeignKey(o => o.RoundId);
        //}

        //void MapManyToMany(DbModelBuilder modelBuilder)
        //{
        //    // many-to-many
        //    //modelBuilder.Entity<User>()
        //    //    .HasMany(o => o.AccessibleCampaigns)
        //    //    .WithMany(o => o.UsersWithAccess)
        //    //    .Map(m =>
        //    //    {
        //    //        m.ToTable("UserCampaignPermission");
        //    //        m.MapLeftKey("UserId");
        //    //        m.MapRightKey("CampaignId");
        //    //    });


        //    //modelBuilder.Entity<Campaign>()
        //    //    .HasMany(o => o.UsersWithAccess)
        //    //    .WithMany(o => o.AccessibleCampaigns)
        //    //    .Map(m =>
        //    //    {
        //    //        m.ToTable("UserCampaignPermission");
        //    //        m.MapLeftKey("CampaignId");
        //    //        m.MapRightKey("UserId");
        //    //    });
        //}

        #endregion
    }
}
