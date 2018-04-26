using Stomatology.Web.Models;
using Stomatology.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stomatology.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Reporting()
        {
            var currentDate = DateTime.Now.Date;
            var startDate = currentDate.StartOfWeek(DayOfWeek.Monday);
            var endDate = startDate.AddDays(7);
            var customers = db.Customers.ToList();
            var newCustomers = customers.Where(c => c.CreateDate >= startDate && c.CreateDate < endDate);

            var staff = db.Staff.ToList();
            var newStaff = staff.Where(c => c.CreateDate >= startDate && c.CreateDate < endDate);

            var appointments = db.Appointments.ToList();
            var newAppointments = appointments.Where(c => c.CreateDate >= startDate && c.CreateDate < endDate);

            var amount = appointments.SelectMany(a => a.Services.Select(x => x.Cost)).Sum();
            var amountWeek = appointments.Where(c => c.CreateDate >= startDate && c.CreateDate < endDate)
                .SelectMany(a => a.Services.Select(x => x.Cost)).Sum();
            var amountToday = appointments.Where(c => c.CreateDate == currentDate)
                .SelectMany(a => a.Services.Select(x => x.Cost)).Sum();
            var lastAmountDate = appointments.Max(x => x.CreateDate);
            var lastAppointment = appointments.FirstOrDefault(x => x.CreateDate == lastAmountDate);
            var lastAmount = lastAppointment != null ? lastAppointment.Services.Select(x => x.Cost).Sum() : 0;

            var viewModel = new ReportingViewModel
            {
                Customers = customers.Count(),
                NewCustomers = newCustomers.Count(),
                Staff = staff.Count(),
                NewStaff = newStaff.Count(),
                Appointments = appointments.Count(),
                NewAppointments = newAppointments.Count(),
                WeekAppointments = newAppointments.Count(),
                CustomersByFriends = customers.Count(c => c.SourceType == SourceType.Friends),
                CustomersBySite = customers.Count(c => c.SourceType == SourceType.Site),
                CustomersByAdvert = customers.Count(c => c.SourceType == SourceType.Advert),
                TotalAmount = amount,
                AmountWeek = amountWeek,
                AmountToday = amountToday,
                LastAmount = lastAmount
            };

            return View(viewModel);
        }

        public ActionResult GetReportBySource()
        {
            var currentDate = DateTime.Now.Date;
            var startDate = currentDate.StartOfWeek(DayOfWeek.Monday);
            var endDate = startDate.AddDays(7);
            var customers = db.Customers.ToList();

            var customersByFriends = customers.Count(c => c.SourceType == SourceType.Friends);
            var customersBySite = customers.Count(c => c.SourceType == SourceType.Site);
            var customersByAdvert = customers.Count(c => c.SourceType == SourceType.Advert);

            return Json(new[] 
            {
                new { label = "Друзья", data = customersByFriends, color = "#2897CB" },
                new { label = "Сайт", data = customersBySite, color = "#62A83B" },
                new { label = "Реклама", data = customersByAdvert, color = "#DEAB34" }
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Appointment()
        {
            return View();
        }

        public ActionResult AddAppointment()
        {
            return View();
        }

        public ActionResult Clients()
        {
            return View();
        }

        public ActionResult AddClient()
        {
            return View();
        }

        public ActionResult EditClient()
        {
            return View();
        }

        public ActionResult ShowClient()
        {
            return View();
        }

        public ActionResult Employees()
        {
            return View();
        }

        public ActionResult AddEmployee()
        {
            return View();
        }

        public ActionResult EditEmployee()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult AddService()
        {
            return View();
        }

        public ActionResult EditService()
        {
            return View();
        }

        public ActionResult Materials()
        {
            return View();
        }
    }
}