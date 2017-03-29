using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVSE.Web.HolidayManagement.Models
{
    public class EmployeeViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public String FirstName { get; set; }

        [Required]
        public String LastName { get; set; }

        public String EmailAddress { get; set; }

        public DateTime? HireDate { get; set; }

        public int? TeamId { get; set; }

        public SelectList Teams { get; set; }

        [Required, Range(1, 100)]
        public int? DaysAvailable { get; set; }
    }
}