using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVSE.Web.HolidayManagement.Models
{
    public class HolidayOverviewViewModel
    {
        public String LoggedInUserName { get; set; }

        public MonthlyCalendarViewModel[] MonthlyCalendars { get; set; }

        public RequestViewModel RequestViewModel { get; set; }
    }
}