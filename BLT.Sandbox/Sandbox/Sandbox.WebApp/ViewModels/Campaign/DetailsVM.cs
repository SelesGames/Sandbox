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

        public DetailsVM() { }

        public DetailsVM(DataContext context, string campaignName)
        {
            this.context = context;
            this.campaignName = campaignName;
        }

        public async Task Load()
        {
            var group = await context.Campaigns
               .WithName(campaignName)
               .Select(o => 
                   new 
                    {
                        Name = o.Name,
                        ImageUrl = o.ImageUrl,
                        LatestProjectTime = o.LatestProjectTime,
                        Projects = o.Projects
                            .Select(c => new BoxContainerVM { Name = c.Name, ImageUrl = c.ImageUrl })
                            .ToList()
                    })
               .SingleOrDefaultAsync();

            this.Name = group.Name;
            this.ImageUrl = group.ImageUrl;
            this.LatestProjectTime = group.LatestProjectTime.ToString();
            this.Projects = group.Projects.ToObservableCollection();
        }
    }
}