using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stomatology.Web.Models
{
    public class ServiceViewModel
    {
        public int ServiceId { get; set; }

        [Required]
        public string ServiceName { get; set; }

        [Required]
        public decimal Cost { get; set; }
    }
}