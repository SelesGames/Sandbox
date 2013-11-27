using Sandbox.WebApp.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sandbox.WebApp.Controllers
{
    public class ProjectController : ControllerBase
    {
       
        public Task<ActionResult> Details(string campaignName, string projectName, string roundNumber)
        {
            return View(new DetailsVM(db, campaignName, projectName, roundNumber));
        }

	}
}