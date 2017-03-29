using DVSE.DAL.HolidayManagement.EF.UnitOfWork;
using DVSE.DAL.HolidayManagement.Entity;
using DVSE.Web.HolidayManagement.Infrastructure.Authentication;
using DVSE.Web.HolidayManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DVSE.Web.HolidayManagement.Helpers;

namespace DVSE.Web.HolidayManagement.Controllers
{
    [Authorize(Roles = "AdminUser")]
    public partial class ManagementController : BaseController
    {
        public ManagementController(IHMUnitOfWork hmUnitOfWork, IDomainUserProvider domainUserProvider)
            : base(hmUnitOfWork, domainUserProvider)
        {
        }

        public virtual ActionResult Index()
        {
            var vm = new ManagementIndexViewModel
            {
                HolidayPeriodViewModel = new HolidayPeriodViewModel
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Purposes = new SelectList(_hmUnitOfWork.PurposeRepository.GetAll(), "Id", "Description"),
                }
            };

            return View(MVC.Management.Views.Index, vm);
        }

        public int GetWorkDaysBetween(DateTime startDate, DateTime endDate)
        {
            var result = endDate - startDate;

            return result.Days;
        }

        public virtual ActionResult GetEmployees()
        {
            var employees = _hmUnitOfWork.EmployeeRepository.GetAll().ToList();

            var currentYear = DateTime.Now.Year;

            var vm = new GridDataJson
            {
                records = employees.Count(),
                rows = employees.Select(x =>
                {
                    var currentHolidayInformation = x.HolidayInformations.SingleOrDefault(y => y.Year == currentYear);

                    var days = 
                        x.HolidayPeriods
                            .Where(y => y.CancelDate == null)
                            .Select(y => GetWorkDaysBetween(y.StartDate, y.EndDate))
                            .Sum();

                    var row = new GridRowJson
                    {
                        id = x.Id,
                        cell = new object[] 
                        {
                            x.FirstName,
                            x.LastName,
                            x.EmailAddress,
                            x.Team != null ? x.Team.Name : "na",
                            currentHolidayInformation != null ? currentHolidayInformation.DaysAvailable.ToString () : "na",
                            currentHolidayInformation != null ? (currentHolidayInformation.DaysAvailable - days).ToString() : "na"
                        }
                    };

                    return row;
                }).ToList()
            };

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult EditEmployee(int id)
        {
            var employee = _hmUnitOfWork.EmployeeRepository.GetSingle(id);

            if (employee == null)
            {
                return Json(new { success = false, message = "Employee is not found." });
            }

            var currentHolidayInformation = employee.HolidayInformations.SingleOrDefault(x => x.Year == DateTime.Now.Year);

            var employeeVM = new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                EmailAddress = employee.EmailAddress,
                HireDate = employee.HireDate,
                TeamId  = employee.TeamId,
                Teams = new SelectList(_hmUnitOfWork.TeamRepository.GetAll(), "Id", "Name"),
                DaysAvailable = currentHolidayInformation != null ? currentHolidayInformation.DaysAvailable : (int?)null
            };

            var json = new 
            {
                success = true,
                content = base.RenderRazorViewToString(MVC.Management.Views._Employee, employeeVM)
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult EditEmployee(EmployeeViewModel employeeVM) 
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Data is not valid." });
            }

            var employee = _hmUnitOfWork.EmployeeRepository.GetSingle(employeeVM.Id);

            if (employee == null)
            {
                return Json(new { success = false, message = "Employee is not found." });
            }

            employee.FirstName = employeeVM.FirstName;
            employee.LastName = employeeVM.LastName;
            employee.EmailAddress = employeeVM.EmailAddress;
            employee.HireDate = employeeVM.HireDate;
            employee.TeamId = employeeVM.TeamId;

            var currentHolidayInformation = employee.HolidayInformations.SingleOrDefault(x => x.Year == DateTime.Now.Year);

            if (currentHolidayInformation == null)
            {
                currentHolidayInformation = new HolidayInformation
                {
                    DaysAvailable = employeeVM.DaysAvailable.Value,
                    Employee = employee
                };

                _hmUnitOfWork.HolidayInformationRepository.Add(currentHolidayInformation);
            }
            else
                currentHolidayInformation.DaysAvailable = employeeVM.DaysAvailable.Value;

            _hmUnitOfWork.Save();

            return Json(new { success = true });
        }

        public virtual ActionResult GetHolidaysForEmployee(int id, bool forCurrentYear)
        {
            var employee = _hmUnitOfWork.EmployeeRepository.GetSingle(id);
            var holidays = employee.HolidayPeriods.Where(x => !forCurrentYear || x.StartDate.Year == DateTime.Now.Year).ToList();

            var vm = new GridDataJson
            {
                records = holidays.Count(),
                rows = holidays.Select(x => new GridRowJson
                {
                    id = x.Id,
                    cell = new object[] 
                    {
                        x.StartDate.ToString("dd-MM-yyyy"),
                        x.EndDate.ToString("dd-MM-yyyy"),
                        x.Purpose.Description,
                        x.CancelDate != null,
                        x.CancelDate != null ? x.CancelDate.Value.ToString("dd-MM-yyyy") : null,
                        x.Note,
                    }
                }).ToList()
            };

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult EditHolidayPeriod(HolidayPeriodViewModel holidayPeriodVM)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Data is not valid." });
            }

            foreach (var employeeId in holidayPeriodVM.SelectedEmployeeIds)
            {
                var employee = _hmUnitOfWork.EmployeeRepository.GetSingle(employeeId);

                if (employee != null)
                {
                    var holidayPeriod = new HolidayPeriod
                    {
                        StartDate = holidayPeriodVM.StartDate.Value,
                        EndDate = holidayPeriodVM.EndDate.Value,
                        Note = holidayPeriodVM.Note,
                        EmployeeId = employee.Id,
                        PurposeId = holidayPeriodVM.PurposeId.Value
                    };

                    _hmUnitOfWork.HolidayPeriodRepository.Add(holidayPeriod);
                }
            }

            _hmUnitOfWork.Save();

            return Json(new { success = true });
        }

        [HttpPost]
        public virtual ActionResult CancelHolidayPeriod(int id, String note)
        {
            var holidayPeriod = _hmUnitOfWork.HolidayPeriodRepository.GetSingle(id);

            if (holidayPeriod == null)
            {
                return Json(new { success = false, message = "Holiday period was not found." });
            }

            if (holidayPeriod.EndDate < DateTime.Now || holidayPeriod.StartDate < DateTime.Now)
            {
                return Json(new { success = false, message = "Cannot cancel this holiday period." });
            }

            holidayPeriod.CancelDate = DateTime.Now;
            holidayPeriod.Note = note;

            _hmUnitOfWork.Save();

            return Json(new { success = true });
        }

        [HttpPost]
        public virtual ActionResult UndoCancelHolidayPeriod(int id, String note)
        {
            var holidayPeriod = _hmUnitOfWork.HolidayPeriodRepository.GetSingle(id);

            if (holidayPeriod == null)
            {
                return Json(new { success = false, message = "Holiday period was not found." });
            }

            holidayPeriod.CancelDate = null;
            holidayPeriod.Note = note;

            _hmUnitOfWork.Save();

            return Json(new { success = true });
        }

        public virtual ActionResult GetOverviewForEmployee(int id)
        {
            var employee = _hmUnitOfWork.EmployeeRepository.GetSingle(id);

            var calendarsVM = base.CreateMonthlyCalendarViewModels(employee);

            var json = new
            {
                success = true,
                content = base.RenderRazorViewToString(MVC.Shared.Views._Calendars, calendarsVM)
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}
