using AutoMapper;
using Stomatology.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Stomatology.Web.Controllers
{
    public class StaffController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Staff
        public async Task<ActionResult> Index()
        {
            var staffs = await db.Staff.Where(x => x.DeleteDate == null).ToListAsync();
            var viewModels = Mapper.Map<List<StaffViewModel>>(staffs);

            return View(viewModels);
        }

        // GET: Staff/Create
        public ActionResult Create()
        {
            var viewModel = new StaffViewModel();

            return View(viewModel);
        }

        // POST: Staff/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StaffViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var staff = Mapper.Map<Staff>(viewModel);

                db.Staff.Add(staff);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Staff/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var staff = await db.Staff.FindAsync(id);
            if (staff == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<StaffViewModel>(staff);

            return View(viewModel);
        }

        // POST: Staff/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StaffViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var staff = await db.Staff.FindAsync(viewModel.StaffId);

                Mapper.Map(viewModel, staff);

                db.Entry(staff).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Staff/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var staff = await db.Staff.FindAsync(id);
            if (staff == null)
            {
                return HttpNotFound();
            }

            staff.DeleteDate = DateTime.Now;

            db.Entry(staff).State = EntityState.Modified;

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var staff = await db.Staff.FindAsync(id);
            db.Staff.Remove(staff);
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
