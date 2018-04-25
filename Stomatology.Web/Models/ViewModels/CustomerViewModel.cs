using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stomatology.Web.Models
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public int? Age { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public SourceType SourceType { get; set; }
        public string SourceTypeName
        {
            get
            {
                var names = new[] { "Друзья", "Сайт", "Реклама" };
                return names[(int) SourceType];
            }
        }

        public List<AppointmentViewModel> Appointments { get; set; }
    }
}