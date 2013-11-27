using Sandbox.Data;
using Sandbox.Data.Entity;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Sandbox.WebApp.ViewModels.Project
{
    public class DetailsVM : IViewModel
    {
        DataContext context;
        string campaignName;
        string projectName;
        string roundNumber;
        Guid userId = Guid.Parse("9063bb54f4444eeeb719ae2e4d9edfd0");

        public string CampaignName { get { return this.campaignName; } }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public Round SelectedRound { get; set; }
        public ObservableCollection<Content> RoundContents { get; set; }
        public ObservableCollection<Round> Rounds { get; set; }

        public DetailsVM(DataContext context, string campaignName, string projectName)
        {
            this.context = context;
            this.campaignName = campaignName;
            this.projectName = projectName;
        }

        public DetailsVM(DataContext context, string campaignName, string projectName, string roundNumber)
            : this(context, campaignName, projectName)
        {
            this.roundNumber = roundNumber;
        }

        public async Task Load()
        {
            var project = await context.Projects
                .WithName(projectName)
                .Where(o => context.Users
                    .Where(u => u.Id == userId)
                    .SelectMany(u => u.AccessibleProjects)
                    .Any(p => p.ProjectId == o.Id && p.Project.Campaign.Name.Equals(campaignName)))
            
            
            //var project = await context.Users
            //    .WithId(userId)
            //    .SelectMany(o => o.AccessibleProjects)
            //    .Select(o => o.Project.Campaign)
            //    .WithName(campaignName).Distinct()
            //    .SelectMany(o => o.Projects)
            //    .WithName(projectName).Distinct()
                .Include(o => o.Rounds.Select(r => r.Contents))
                .Select(o =>
                    new
                    {
                        Name = o.Name,
                        ImageUrl = o.ImageUrl,
                        Rounds = o.Rounds.OrderByDescending(r => r.CreatedOn)
                    })
               .SingleOrDefaultAsync();

            if (project == null)
            {
                throw new Exception("no matching project");
            }

            this.Name = project.Name;
            this.ImageUrl = project.ImageUrl;
            this.Rounds = project.Rounds.ToObservableCollection();
            this.SelectedRound = (null != roundNumber)
                ? this.Rounds.Where(r => r.RoundNumber == this.roundNumber).FirstOrDefault() 
                : this.Rounds.FirstOrDefault();
            this.RoundContents = Rounds.Take(1)
                .SelectMany(o => o.Contents)
                .OrderBy(o => o.ContentIndex)
                .ToObservableCollection();
        }




        #region override ToString method

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, ImageUrl);
        }

        #endregion
    }
}