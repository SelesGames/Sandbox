using Sandbox.Data.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Sandbox.WebApp.ViewModels.Campaign
{
    public class IndexVM
    {
        DataContext context;

        public ObservableCollection<BoxContainerVM> Campaigns { get; set; }

        public IndexVM(DataContext context)
        {
            this.context = context;
            Campaigns = new ObservableCollection<BoxContainerVM>();
        }

        public async Task Load()
        {

        }
    }
}