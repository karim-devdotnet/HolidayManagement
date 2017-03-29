using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVSE.Web.HolidayManagement.Models
{
    public class MonthlyCalendarViewModel
    {
        public String Month { get; set; }

        public int MonthIndex { get; set; }

        public int FirstDay { get; set; }

        public int NumberOfDays { get; set; }

        public IList<int> Holidays { get; set; }

        public IList<int> Requests { get; set; }

        public MonthlyCalendarViewModel()
        {
        }
    }
}