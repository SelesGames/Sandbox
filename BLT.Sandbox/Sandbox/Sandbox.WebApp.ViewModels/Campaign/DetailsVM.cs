using Sandbox.Data;
using Sandbox.Data.Entity;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Sandbox.WebApp.ViewModels.Campaign
{
    public class DetailsVM : IViewModel
    {
        DataContext context;
        string campaignName;
        Guid userId = Guid.Parse("9063bb54f4444eeeb719ae2e4d9edfd0");

        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string LatestProjectTime { get; set; }
        public ObservableCollection<BoxContainerVM> Projects { get; set; }

        public DetailsVM(DataContext context, string campaignName)
        {
            this.context = context;
            this.campaignName = campaignName;
        }

        public async Task Load()
        {
            var campaign = await context.Users
                .WithId(userId)
                .SelectMany(o => o.AccessibleProjects)
                .Select(o => o.Project.Campaign)
                .WithName(campaignName)
                .Distinct()
                .Select(o => 
                    new 
                    {
                        Name = o.Name,
                        ImageUrl = o.ImageUrl,
                        LatestProjectTime = o.LatestProjectTime,
                        Projects = o.Projects
                            .OrderByDescending(c => c.LatestRoundModified)
                            .Select(c => new BoxContainerVM { Name = c.Name, ImageUrl = c.ImageUrl })
                            .ToList()
                    })
               .SingleOrDefaultAsync();

            if (campaign == null)
            {
                throw new Exception("no matching campaign");
            }

            this.Name = campaign.Name;
            this.ImageUrl = campaign.ImageUrl;
            this.LatestProjectTime = campaign.LatestProjectTime.ToString();
            this.Projects = campaign.Projects.ToObservableCollection();
        }
    }
}