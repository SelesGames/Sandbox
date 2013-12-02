using Sandbox.Data;
using Sandbox.Data.Entity;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Sandbox.WebApp.ViewModels.Campaign
{
    public class IndexVM : IViewModel
    {
        DataContext context;
        Guid userId = Guid.Parse("9063bb54f4444eeeb719ae2e4d9edfd0");

        public ObservableCollection<GroupedCampaigns> Campaigns { get; set; }

        public IndexVM(DataContext context)
        {
            this.context = context;
            Campaigns = new ObservableCollection<GroupedCampaigns>();
        }

        public async Task Load()
        {
            var campaigns = await context.Users
                .WithId(userId)
                .SelectMany(o => o.AccessibleProjects)
                .Select(o => o.Project.Campaign)
                //.Include(o => o.Group)
                .Distinct()
                .Select(o => new { Name = o.Name, ImageUrl = o.ImageUrl, GroupName = o.Group.Name })
                .ToListAsync();

            // do the grouping in code and not SQL
            var groupedCampaigns = campaigns
                .GroupBy(o => o.GroupName)
                .Select(o =>
                    new GroupedCampaigns
                    {
                        GroupName = o.Key,
                        Campaigns = o
                            .Select(c => new BoxContainerVM { Name = c.Name, ImageUrl = c.ImageUrl })
                            .ToObservableCollection()
                    })
                .ToList();

            foreach (var group in groupedCampaigns)
            {
                Campaigns.Add(group);
            }
        }
    }

    public class GroupedCampaigns
    {
        public string GroupName { get; set; }
        public ObservableCollection<BoxContainerVM> Campaigns { get; set; }
    }
}