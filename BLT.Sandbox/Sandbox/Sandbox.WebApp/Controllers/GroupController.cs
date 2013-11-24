using Sandbox.Data;
using Sandbox.WebApp.ViewModels.Group;
using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;

namespace Sandbox.WebApp.Controllers
{
    //[Authorize]
    public class GroupController : ControllerBase
    {
        Guid userId = Guid.Parse("9063bb54f4444eeeb719ae2e4d9edfd0");




        #region Index (list of groups user has permissions to)

        // GET: /Groups/
        public Task<ActionResult> Index()
        {
            return View(new IndexVM(db));
        }

        #endregion




        #region Details page for a specific group

        // GET: /Groups/Details/{name}
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




        #region Create a new Group

        // GET: /Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,LogoUrl")] Group Group)
        {
            if (ModelState.IsValid)
            {
                Group.Id = Guid.NewGuid();
                db.Groups.Add(Group);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(Group);
        }

        #endregion




        #region Edit a Group

        // GET: /Groups/Edit/{name}
        public async Task<ActionResult> Edit(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group Group = await db.Groups.WithName(name).SingleOrDefaultAsync();
            if (Group == null)
            {
                return HttpNotFound();
            }
            return View(Group);
        }

        // POST: /Groups/Edit/{name}
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,LogoUrl")] Group Group)
        {
            if (ModelState.IsValid)
            {
                var dbGroup = await db.Groups.WithId(Group.Id).SingleOrDefaultAsync();

                dbGroup.State = Group.State;
                dbGroup.Name = Group.Name;
                dbGroup.LogoUrl = dbGroup.LogoUrl;

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(Group);
        }

        #endregion




        #region Delete a group

        // GET: /Groups/Delete/{name}
        public async Task<ActionResult> Delete(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var group = await db.Groups.WithName(name).SingleOrDefaultAsync();
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: /Groups/Delete/{name}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string name)
        {
            var group = await db.Groups.WithName(name).SingleOrDefaultAsync();
            db.Groups.Remove(group);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
