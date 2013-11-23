using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sandbox.Data;
using Sandbox.Data.Entity;
using Sandbox.WebApp.ViewModels.Group;

namespace Sandbox.WebApp.Controllers
{
    //[Authorize]
    public class GroupController : Controller
    {
        DataContext db = new DataContext();
        Guid userId = Guid.Parse("9063bb54f4444eeeb719ae2e4d9edfd0");

        // GET: /Groups/
        public async Task<ActionResult> Index()
        {
            var vm = new IndexVM(db);
            await vm.Load();
            return View(vm);
        }

        // GET: /Groups/Details/5
        public async Task<ActionResult> Details(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vm = new DetailsVM(db, name);
            await vm.Load();
            return View(vm);

            //if (Group == null)
            //{
            //    return HttpNotFound();
            //}
        }

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
        public async Task<ActionResult> Create([Bind(Include="Id,Name,LogoUrl,ProjectCount,LatestProjectTime,LatestProjectName,LatestProjectId")] Group Group)
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

        // GET: /Groups/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group Group = await db.Groups.FindAsync(id);
            if (Group == null)
            {
                return HttpNotFound();
            }
            return View(Group);
        }

        // POST: /Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,LogoUrl,ProjectCount,LatestProjectTime,LatestProjectName,LatestProjectId")] Group Group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Group).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(Group);
        }

        // GET: /Groups/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group Group = await db.Groups.FindAsync(id);
            if (Group == null)
            {
                return HttpNotFound();
            }
            return View(Group);
        }

        // POST: /Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Group Group = await db.Groups.FindAsync(id);
            db.Groups.Remove(Group);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
