using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stomatology.Web.Models
{
    public class Appointment
    {
        public Appointment()
        {
            CreateDate = DateTime.Now;
        }

        public int AppointmentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int StaffId { get; set; }
        public virtual Staff Staff { get; set; }

        public string Comment { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual List<Service> Services { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}