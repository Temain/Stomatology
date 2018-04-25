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
    public class CustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customer
        public async Task<ActionResult> Index()
        {
            var customers = await db.Customers.Where(x => x.DeleteDate == null).ToListAsync();
            var viewModels = Mapper.Map<List<CustomerViewModel>>(customers);

            return View(viewModels);
        }

        // GET: Customer
        public async Task<ActionResult> Show(int id)
        {
            var customer = await db.Customers.SingleOrDefaultAsync(x => x.CustomerId == id && x.DeleteDate == null);
            var viewModel = Mapper.Map<CustomerViewModel>(customer);

            return View(viewModel);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            var viewModel = new CustomerViewModel();

            return View(viewModel);
        }

        // POST: Customer/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = Mapper.Map<Customer>(viewModel);

                db.Customers.Add(customer);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Customer/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<CustomerViewModel>(customer);

            return View(viewModel);
        }

        // POST: Customer/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = await db.Customers.FindAsync(viewModel.CustomerId);

                Mapper.Map(viewModel, customer);

                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Customer/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            customer.DeleteDate = DateTime.Now;

            db.Entry(customer).State = EntityState.Modified;

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var customer = await db.Customers.FindAsync(id);
            db.Customers.Remove(customer);
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
