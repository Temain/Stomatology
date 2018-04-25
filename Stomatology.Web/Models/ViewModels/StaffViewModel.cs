using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stomatology.Web.Models
{
    public class StaffViewModel
    {
        public int StaffId { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        [Required]
        public string Speciality { get; set; }

        public string Phone { get; set; }
    }
}