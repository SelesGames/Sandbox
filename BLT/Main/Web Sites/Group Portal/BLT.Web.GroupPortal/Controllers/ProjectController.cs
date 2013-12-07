using BLT.ClientExtranet.Data.EntityFramework;
using BLT.ClientExtranet.ViewModels.Project;
using BLT.Core.Logging.PlatformLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BLT.ClientExtranet.Web.GroupPortal.Controllers
{
    public class ProjectController : ControllerBase
    {
      
        public Task<ActionResult> Details(string campaignName, string projectName, string roundNumber)
        {
            //PlatformLog.LogActivity(GroupLogActivity.RoundViewed, "", "");

            return View(new DetailsVM(db, campaignName, projectName, roundNumber));
        }
	}
}