using DVSE.DAL.HolidayManagement.EF.UnitOfWork;
using DVSE.DAL.HolidayManagement.Entity;
using DVSE.Web.HolidayManagement.Infrastructure.Authentication;
using DVSE.Web.HolidayManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVSE.Web.HolidayManagement.Controllers
{
    public partial class BaseController : Controller
    {
        protected IHMUnitOfWork _hmUnitOfWork;

        protected IDomainUserProvider _domainUserProvider;

        protected Employee CurrentEmployee
        {
            get
            {
                var loggedInUsername = _domainUserProvider.GetLoggedInUsername();

                return 
                    _hmUnitOfWork
                        .EmployeeRepository
                        .FindBy(x => x.ADName == loggedInUsername)
                        .SingleOrDefault();
            }
        }

        public BaseController()
        { }

        public BaseController(IHMUnitOfWork hmUnitOfWork, IDomainUserProvider domainUserProvider)
        {
            _hmUnitOfWork = hmUnitOfWork;

            _domainUserProvider = domainUserProvider;
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);

                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);

                return sw.GetStringBuilder().ToString();
            }
        }

        protected MonthlyCalendarViewModel[] CreateMonthlyCalendarViewModels(Employee employee)
        {
            var months = new MonthlyCalendarViewModel[12];

            var now = DateTime.Now;

            for (var i = 0; i < 12; ++i)
            {
                var month = new DateTime(now.Year, i + 1, 1);

                months[i] = new MonthlyCalendarViewModel
                {
                    Month = month.ToString("MMMM"),
                    MonthIndex = i,
                    FirstDay = (int)month.DayOfWeek,
                    NumberOfDays = DateTime.DaysInMonth(now.Year, i + 1),
                    Holidays = new List<int>(),
                    Requests = new List<int>(),
                };
            }

            var holidayPeriods = employee.HolidayPeriods.Where(x => x.StartDate.Year == DateTime.Now.Year && x.CancelDate == null);

            foreach (var holidayPeriod in holidayPeriods)
            {
                for (var date = holidayPeriod.StartDate; date <= holidayPeriod.EndDate; date = date.AddDays(1))
                {
                    months[date.Month - 1].Holidays.Add(date.Day);
                }
            }

            var requests = employee.Requests.Where(x => x.StartDate.Year == DateTime.Now.Year && x.CancelDate == null);

            foreach (var request in requests)
            {
                for (var date = request.StartDate; date <= request.EndDate; date = date.AddDays(1))
                {
                    months[date.Month - 1].Requests.Add(date.Day);
                }
            }

            return months;
        }
    }
}
