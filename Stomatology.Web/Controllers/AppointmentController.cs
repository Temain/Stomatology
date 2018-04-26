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
    public class AppointmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointment
        public async Task<ActionResult> Index(int? weekOffset = null)
        {
            var currentDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            if (weekOffset != null)
            {
                currentDate = currentDate.AddDays(7 * weekOffset ?? 0);
            }

            ViewBag.CurrentDate = currentDate;
            ViewBag.WeekOffset = weekOffset ?? 0;

            var endWeekDate = currentDate.AddDays(7);
            var appointments = await db.Appointments.Where(x => x.StartDate >= DbFunctions.TruncateTime(currentDate) && x.StartDate <= DbFunctions.TruncateTime(endWeekDate) && x.DeleteDate == null).ToListAsync();
            var viewModels = Mapper.Map<List<AppointmentViewModel>>(appointments);

            return View(viewModels);
        }

        // GET: Appointment/Create
        public async Task<ActionResult> Create()
        {
            var viewModel = new AppointmentViewModel();

            await CreateSelectLists();

            return View(viewModel);
        }

        // POST: Appointment/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AppointmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var appointment = Mapper.Map<Appointment>(viewModel);

                if (viewModel.SelectedServices != null && viewModel.SelectedServices.Count() > 0)
                {
                    var services = await db.Services.Where(m => viewModel.SelectedServices.Contains(m.ServiceId) && m.DeleteDate == null)
                        .ToListAsync();

                    appointment.Services = services;
                }

                db.Appointments.Add(appointment);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Appointment/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<AppointmentViewModel>(appointment);
            viewModel.SelectedServices = appointment.Services.Select(m => m.ServiceId).ToList();

            await CreateSelectLists();

            return View(viewModel);
        }

        // POST: Appointment/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AppointmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var appointment = await db.Appointments.FindAsync(viewModel.AppointmentId);

                Mapper.Map(viewModel, appointment);

                if (viewModel.SelectedServices != null && viewModel.SelectedServices.Count() > 0)
                {
                    var materials = await db.Services.Where(m => viewModel.SelectedServices.Contains(m.ServiceId) && m.DeleteDate == null)
                        .ToListAsync();

                    appointment.Services.Clear();
                    appointment.Services.AddRange(materials);
                }

                db.Entry(appointment).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Appointment/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }

            appointment.DeleteDate = DateTime.Now;

            db.Entry(appointment).State = EntityState.Modified;

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var appointment = await db.Appointments.FindAsync(id);
            db.Appointments.Remove(appointment);
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

        private async Task CreateSelectLists()
        {
            var customers = await db.Customers.Where(x => x.DeleteDate == null)
                .Select(x => new {
                    x.CustomerId,
                    CustomerName = x.LastName + " " + (x.FirstName != null ? x.FirstName : "") + " "
                        + (x.MiddleName != null ? x.MiddleName : "")
                })
                .ToListAsync();
            ViewBag.Customers = new SelectList(customers, "CustomerId", "CustomerName");

            var staff = await db.Staff.Where(x => x.DeleteDate == null)
                .Select(x => new {
                    x.StaffId,
                    StaffName = x.LastName + " " + (x.FirstName != null ? x.FirstName : "") + " "
                        + (x.MiddleName != null ? x.MiddleName : "")
                        + (", " + x.Speciality ?? "")
                })
                .ToListAsync();
            ViewBag.Staff = new SelectList(staff, "StaffId", "StaffName");

            ViewBag.Materials = await db.Services.Where(x => x.DeleteDate == null)
               .Select(x => new ServiceViewModel
               {
                   ServiceId = x.ServiceId,
                   ServiceName = x.ServiceName + ", " + (x.Cost + "руб." ?? "")
               })
               .ToListAsync();
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
