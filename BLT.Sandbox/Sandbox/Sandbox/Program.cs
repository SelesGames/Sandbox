using Sandbox.Data;
using Sandbox.Data.Entity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize().Wait();
            //RunTest().Wait();

            while (true)
                Console.Read();
        }

        //private static async System.Threading.Tasks.Task RunTest()
        //{
        //    var dataContext = CreateContext();

        //    var userId = Guid.NewGuid();
        //    var categoryId = Guid.NewGuid();

        //    var userPostsByCategory = await dataContext
        //        .Users
        //        //.Where(o => userId == o.UserId)
        //        .SelectMany(o => o.Campaigns)
        //        .SelectMany(o => o.Projects)
        //        .Where(o => categoryId == o.CategoryId)
        //        .Select(o => o.Name)
        //        .AsNoTracking()
        //        .ToListAsync();

        //    var newUser = new User
        //    {
        //        UserId = Guid.NewGuid(),
        //    };
        //    dataContext.Users.Add(newUser);
        //    await dataContext.SaveChangesAsync();

            

        //    var users = await dataContext.Users.Select(o => o.Id).ToListAsync();
        //    foreach (var user in users)
        //        Console.WriteLine(user);
        //}

        static async Task Initialize()
        {
            await CreateClients();
            await OutputClients();
            await AddCampaignsToClients();
            await OutputCampaigns();
            await AddProjectsToCampaigns();
            await OutputProjects();

            await DeleteClient();

            await OutputClients();
            await OutputCampaigns();
            await OutputProjects();
        }

        static async Task CreateClients()
        {
            using (var context = CreateContext())
            {
                context.Clients.AddRange(new[] {
                    new Client
                    {
                        Id = Guid.Parse("1d5067434147480ab826efb7e11939a8"),
                        Name = "CBS",
                    },
                    new Client
                    {
                        Id = Guid.Parse("b5948857f8b44a529a375cff56788797"),
                        Name = "Marvel",
                    },
                    new Client
                    {
                        Id = Guid.Parse("d3a8298754cb462fbc0096285cca2623"),
                        Name = "20th Century Fox",
                    },
                });

                await context.SaveChangesAsync();
            }
        }

        static async Task OutputClients()
        {
            using (var context = CreateContext())
            {
                Console.WriteLine("CLIENTS:");
                Console.WriteLine("***********");

                var clients = await context.Clients.ToListAsync();

                foreach (var client in clients.Select(o => o.Name))
                    Console.WriteLine(client);

                Console.WriteLine();
            }
        }

        static async Task AddCampaignsToClients()
        {
            using (var context = CreateContext())
            {
                var marvel = await context.Clients.SingleAsync(o => o.Name.Equals("marvel", StringComparison.OrdinalIgnoreCase));

                context.Campaigns.AddRange(new[]
                {
                    new Campaign
                    {
                        Id = Guid.Parse("e17ee3df2a19402784e4568edcfab8e3"),
                        Client = marvel,
                        Name = "Silver Surfer movie",
                    },
                    new Campaign
                    {
                        Id = Guid.Parse("5d893561243149a08b12d33b13334d7e"),
                        Client = marvel,
                        Name = "Avengers 3",
                    },
                    new Campaign
                    {
                        Id = Guid.Parse("208c26eb8fdd44779647445b5e0c0611"),
                        Client = marvel,
                        Name = "Dr. Strange movie",
                    },
                });

                await context.SaveChangesAsync();
            }

            using (var context = CreateContext())
            {
                var cbs = await context.Clients.SingleAsync(o => o.Name.Equals("cbs", StringComparison.OrdinalIgnoreCase));

                cbs.Campaigns.Add(
                    new Campaign
                    {
                        Id = Guid.Parse("601c080013bf45919abdab3a29c3ec1b"),
                        Client = cbs,
                        Name = "How I Met Your Mother",
                    });

                await context.SaveChangesAsync();
            }

            Client fox;

            using (var context = CreateContext())
            {
                fox = await context.Clients.SingleAsync(o => o.Name.Contains("fox"));

                fox.AddCampaign(
                    new Campaign
                    {
                        Id = Guid.Parse("f3ee12fb592b45c0a593f09a9c7f1230"),
                        Name = "The Simpsons",
                    });

                await context.SaveChangesAsync();
            }

            using (var context = CreateContext())
            {
                context.Campaigns.Add(
                    new Campaign
                    {
                        Id = Guid.Parse("a9f7bd5b4599483194ccaf5ba50a163d"),
                        ClientId = fox.Id,
                        Name = "Family Guy",
                    });

                await context.SaveChangesAsync();
            }
        }

        static async Task OutputCampaigns()
        {
            using (var context = CreateContext())
            {
                var campaigns = await context
                    .Campaigns
                    .Include(o => o.Client)  // tells the query to *eager* load Client
                    .Select(o => new { ClientName = o.Client.Name, CampaignName = o.Name })
                    .OrderBy(o => o.ClientName)
                    .ThenBy(o => o.CampaignName)
                    .ToListAsync();

                Console.WriteLine("CAMPAIGNS:");
                Console.WriteLine("***********");

                foreach (var campaign in campaigns)
                    Console.WriteLine("{0} - {1}", campaign.ClientName, campaign.CampaignName);

                Console.WriteLine();
            }
        }

        static async Task AddProjectsToCampaigns()
        {
            using (var context = CreateContext())
            {
                var avengers = await context.Campaigns.SingleAsync(o => o.Name.Contains("avengers"));

                avengers.AddProject(
                    new Project
                    {
                        Id = Guid.Parse("e34256bc5509453d9e969bee2ed9b439"), 
                        Name = "Avengers Blog #1"
                    });

                avengers.AddProject(
                    new Project
                    {
                        Id = Guid.Parse("feddea4607084a2d8fa28a3fe3800678"),
                        Name = "Avengers Blog #2"
                    });

                await context.SaveChangesAsync();
            }

            using (var context = CreateContext())
            {
                var simpsons = await context.Campaigns.SingleAsync(o => o.Name.Contains("simpsons"));

                simpsons.AddProject(
                    new Project
                    {
                        Id = Guid.Parse("c5fd3e6593584593bd49a88e78ffb4cf"),
                        Name = "Simpsons Fox.com site"
                    });

                simpsons.AddProject(
                    new Project
                    {
                        Id = Guid.Parse("5281c1fe5a6040d19d56c986dbaaa529"),
                        Name = "Halloween website"
                    });

                await context.SaveChangesAsync();
            }
        }

        static async Task OutputProjects()
        {
            using (var context = CreateContext())
            {
                var projects = await context
                    .Projects
                    .Include(o => o.Client)  // tells the query to *eager* load Client
                    .Include(o => o.Campaign)
                    .Select(o => new { ClientName = o.Client.Name, CampaignName = o.Campaign.Name, ProjectName = o.Name })
                    .OrderBy(o => o.ClientName)
                    .ThenBy(o => o.CampaignName)
                    .ThenBy(o => o.ProjectName)
                    .ToListAsync();

                Console.WriteLine("PROJECTS:");
                Console.WriteLine("***********");

                foreach (var o in projects)
                    Console.WriteLine("{0} - {1} - {2}", o.ClientName, o.CampaignName, o.ProjectName);

                Console.WriteLine();
            }
        }

        static async Task DeleteClient()
        {
            using (var context = CreateContext())
            {
                var marvel = await context.Clients.SingleAsync(o => o.Name.Contains("marvel"));

                Console.WriteLine("******** DELETING MARVEL ********");

                context.Clients.Remove(marvel);
                await context.SaveChangesAsync();

                Console.WriteLine();
            }
        }


        static DataContext CreateContext()
        {
            return new DataContext();
        }
    }
}
//  24803455