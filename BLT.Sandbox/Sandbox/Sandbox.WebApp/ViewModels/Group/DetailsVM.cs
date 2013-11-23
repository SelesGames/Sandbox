using Sandbox.Data.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Sandbox.WebApp.ViewModels.Group
{
    public class DetailsVM
    {
        DataContext context;
        string groupName;
        Guid userId = Guid.Parse("9063bb54f4444eeeb719ae2e4d9edfd0");

        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string LatestProjectTime { get; set; }
        public ObservableCollection<BoxContainerVM> Campaigns { get; set; }

        public DetailsVM() { }

        public DetailsVM(DataContext context, string groupName)
        {
            this.context = context;
            this.groupName = groupName;
        }

        public async Task Load()
        {
            var group = await context.Groups
               .Where(o => o.Name.Equals(groupName))
               //.Include(o => o.Campaigns)
               .Select(o => 
                   new 
                    {
                        Name = o.Name,
                        LogoUrl = o.LogoUrl,
                        LatestProjectTime = o.LatestProjectTime,
                        Campaigns = o.Campaigns
                            .Select(c => new BoxContainerVM { Name = c.Name, ImageUrl = c.ImageUrl })
                            .ToList()
                    })
               .SingleOrDefaultAsync();

            this.Name = group.Name;
            this.LogoUrl = group.LogoUrl;
            this.LatestProjectTime = group.LatestProjectTime.ToString();
            this.Campaigns = group.Campaigns.ToObservableCollection();
        }
    }
}