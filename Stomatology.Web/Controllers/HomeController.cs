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
        public ActionResult Reporting()
        {
            return View();
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

        public ActionResult ShowClient()
        {
            return View();
        }

        public ActionResult Employees()
        {
            return View();
        }
    }
}