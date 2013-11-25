using Sandbox.Data;
using Sandbox.WebApp.ViewModels.Campaign;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sandbox.WebApp.Controllers
{
    public class CampaignController : ControllerBase
    {
        #region Index (list of groups user has permissions to)

        // GET: /Campaigns/
        public Task<ActionResult> Index()
        {
            return View(new IndexVM(db));
        }

        #endregion




        #region Details page for a specific campaign

        // GET: /Campaigns/Details/{name}
        public async Task<ActionResult> Details(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return await View(new DetailsVM(db, name));

            //if (Group == null)
            //{
            //    return HttpNotFound();
            //}
        }

        #endregion




        #region Create a new Campaign

        // GET: /Campaigns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Campaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,State,Name,ImageUrl")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                campaign.Id = Guid.NewGuid();
                db.Campaigns.Add(campaign);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(campaign);
        }

        #endregion




        #region Edit a Campaign

        // GET: /Campaigns/Edit/{name}
        public async Task<ActionResult> Edit(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = await db.Campaigns.WithName(name).SingleOrDefaultAsync();
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // POST: /Campaigns/Edit/{name}
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,State,Name,ImageUrl")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                var dbCampaign = await db.Campaigns.WithId(campaign.Id).SingleOrDefaultAsync();

                // update via optimistic concurrency, database wins
                // http://msdn.microsoft.com/en-us/data/jj592904.aspx
                bool saveFailed;
                do
                {
                    saveFailed = false;

                    try
                    {
                        dbCampaign.State = campaign.State;
                        dbCampaign.Name = campaign.Name;
                        dbCampaign.ImageUrl = campaign.ImageUrl;

                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        saveFailed = true;

                        // Update the values of the entity that failed to save from the store
                        ex.Entries.Single().Reload();
                    }

                } while (saveFailed);

                return RedirectToAction("Index");
            }
            return View(campaign);
        }

        #endregion




        #region Delete a campaign

        // GET: /Campaigns/Delete/{name}
        public async Task<ActionResult> Delete(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var campaign = await db.Campaigns.WithName(name).SingleOrDefaultAsync();
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // POST: /Campaigns/Delete/{name}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string name)
        {
            var campaign = await db.Campaigns.WithName(name).SingleOrDefaultAsync();
            db.Campaigns.Remove(campaign);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
