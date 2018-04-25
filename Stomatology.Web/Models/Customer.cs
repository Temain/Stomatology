using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stomatology.Web.Models
{
    public class Customer
    {
        public Customer()
        {
            CreateDate = DateTime.Now;
        }

        public int CustomerId { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public int Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public SourceType SourceType { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public virtual List<Appointment> Appointments { get; set; }
    }

    public enum SourceType
    {
        Friends,
        Site,
        Advert
    }
}