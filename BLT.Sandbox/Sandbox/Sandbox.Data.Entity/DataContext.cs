using System.Data.Entity;

namespace Sandbox.Data.Entity
{
    public class DataContext : DbContext
    {
        public DataContext(string connectionString)
            : base(connectionString)
        { 
#if DEBUG
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
#endif
            // DISABLE MODEL SCHEMA MIGRATIONS
            //Database.SetInitializer<DataContext>(null);

            // REQUIRED FOR OPTIMAL QUERIES WHEN SELECTING PRIMARY KEYS
            // outputs "where userId = 'blah'" instead of
            // "where userId = 'blah' and userId is not null"
            Configuration.UseDatabaseNullSemantics = true;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Campaign> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Content> Contents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Client>().HasKey(o => o.ClientId);
            //modelBuilder.Entity<Project>().HasKey(o => o.ProjectId);
            //modelBuilder.Entity<Post>().HasKey(o => o.PostId);
            //modelBuilder.Entity<PostMediaItem>().HasKey(o => o.PostMediaItemId);
            //modelBuilder.Entity<Category>().HasKey(o => o.CategoryId);
            //modelBuilder.Entity<User>().HasKey(o => o.UserId);

            //// Client foreign key mappings
            //modelBuilder.Entity<Client>().HasOptional(o => o.LatestPost);

            //// Project foreign key mappings
            //modelBuilder.Entity<Project>().HasRequired(o => o.Client).WithMany(o => o.Projects).HasForeignKey(o => o.ClientId);
            //modelBuilder.Entity<Project>().HasOptional(o => o.LatestPost);

            //// Post foreign key mappings
            //modelBuilder.Entity<Post>().HasRequired(o => o.Client).WithMany(o => o.Posts).HasForeignKey(o => o.ClientId);
            //modelBuilder.Entity<Post>().HasRequired(o => o.Project).WithMany(o => o.Posts).HasForeignKey(o => o.ProjectId);
            //modelBuilder.Entity<Post>().HasRequired(o => o.Category).WithMany(o => o.Posts).HasForeignKey(o => o.CategoryId);

            //// PostMediaItem foreign key mappings
            //modelBuilder.Entity<PostMediaItem>().HasRequired(o => o.Post).WithMany(o => o.PostMediaItems).HasForeignKey(o => o.PostId);

            modelBuilder.Entity<User>()
                .HasMany(o => o.Clients)
                .WithMany(o => o.Users)
                .Map(m => 
                {
                    m.ToTable("UserClientPermission");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("ClientId");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
