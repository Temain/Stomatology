using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stomatology.Web.Models
{
    public class Staff
    {
        public Staff()
        {
            CreateDate = DateTime.Now;
        }

        public int StaffId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Speciality { get; set; }
        public string Phone { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public virtual List<Appointment> Appointments { get; set; }
    }
}