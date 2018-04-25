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
    public class ServiceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Service
        public async Task<ActionResult> Index()
        {
            var services = await db.Services.Where(x => x.DeleteDate == null).ToListAsync();
            var viewModels = Mapper.Map<List<ServiceViewModel>>(services);

            return View(viewModels);
        }

        // GET: Service/Create
        public ActionResult Create()
        {
            var viewModel = new ServiceViewModel();

            return View(viewModel);
        }

        // POST: Service/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ServiceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var service = Mapper.Map<Service>(viewModel);

                db.Services.Add(service);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Service/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var service = await db.Services.FindAsync(id);
            if (service == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<ServiceViewModel>(service);

            return View(viewModel);
        }

        // POST: Service/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ServiceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var service = await db.Services.FindAsync(viewModel.ServiceId);

                Mapper.Map(viewModel, service);

                db.Entry(service).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Service/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var service = await db.Services.FindAsync(id);
            if (service == null)
            {
                return HttpNotFound();
            }

            service.DeleteDate = DateTime.Now;

            db.Entry(service).State = EntityState.Modified;

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var service = await db.Services.FindAsync(id);
            db.Services.Remove(service);
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
