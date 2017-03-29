using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVSE.Web.HolidayManagement.Models
{
    public class HolidayPeriodViewModel
    {
        public int[] SelectedEmployeeIds { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        public String Note { get; set; }

        [Required]
        public int? PurposeId { get; set; }

        public SelectList Purposes { get; set; }
    }
}