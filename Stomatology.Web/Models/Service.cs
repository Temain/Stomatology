using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stomatology.Web.Models
{
    public class Service
    {
        public Service()
        {
            CreateDate = DateTime.Now;
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal Cost { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public virtual List<Appointment> Appointments { get; set; }
    }
}