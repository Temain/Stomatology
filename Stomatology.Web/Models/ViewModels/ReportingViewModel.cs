using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stomatology.Web.Models.ViewModels
{
    public class ReportingViewModel
    {
        public int Customers { get; set; }
        public int NewCustomers { get; set; }

        public int Appointments { get; set; }
        public int NewAppointments { get; set; }

        public int Staff { get; set; }
        public int NewStaff { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal LastAmount { get; set; }

        public int CustomersByFriends { get; set; }
        public int CustomersBySite { get; set; }
        public int CustomersByAdvert { get; set; }

        public decimal AmountToday { get; set; }
        public decimal AmountWeek { get; set; }

        public int WeekAppointments { get; set; }
    }
}