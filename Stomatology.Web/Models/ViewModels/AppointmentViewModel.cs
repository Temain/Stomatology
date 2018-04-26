using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stomatology.Web.Models
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int StaffId { get; set; }
        public string StaffName { get; set; }

        public string Comment { get; set; }

        public string ApplicationUserId { get; set; }
        public string ApplicationUser { get; set; }

        public List<int> SelectedServices { get; set; }
        public List<ServiceViewModel> Services { get; set; }
    }
}