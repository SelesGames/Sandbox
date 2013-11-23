using Sandbox.Data;
using Sandbox.Data.Entity;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Sandbox.WebApp.ViewModels.Group
{
    public class IndexVM
    {
        DataContext context;
        Guid userId = Guid.Parse("9063bb54f4444eeeb719ae2e4d9edfd0");

        public ObservableCollection<BoxContainerVM> Groups { get; set; }

        public IndexVM(DataContext context)
        {
            this.context = context;
            Groups = new ObservableCollection<BoxContainerVM>();
        }

        public async Task Load()
        {
            var groups = await context.Users
                .WithId(userId)
                .SelectMany(o => o.AccessibleProjects)
                .Select(o => o.Project.Campaign.Group)
                .Distinct()
                .Select(o => new BoxContainerVM {  Name = o.Name, ImageUrl = o.LogoUrl })
                .OrderBy(o => o.Name)
                .ToListAsync();

            foreach (var group in groups)
            {
                Groups.Add(group);
            }
        }
    }
}