using BLT.ClientExtranet.Data.EntityFramework;
using BLT.ClientExtranet.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLT.ClientExtranet.Web.GroupPortal.Controllers
{
    public class ControllerBase : Controller
    {
        protected DataContext db = new DataContext();

        protected async Task<ActionResult> View(IViewModel viewModel)
        {
            await viewModel.Load();
            return base.View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}