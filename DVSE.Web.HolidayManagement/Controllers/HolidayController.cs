using DVSE.DAL.HolidayManagement.EF.UnitOfWork;
using DVSE.DAL.HolidayManagement.Entity;
using DVSE.Web.HolidayManagement.Infrastructure;
using DVSE.Web.HolidayManagement.Infrastructure.Authentication;
using DVSE.Web.HolidayManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DVSE.Web.HolidayManagement.Helpers;

namespace DVSE.Web.HolidayManagement.Controllers
{
    [Authorize(Roles = "NormalUser, AdminUser")]
    public partial class HolidayController : BaseController
    {
        // testing commit from Kinga

        public HolidayController(IHMUnitOfWork hmUnitOfWork, IDomainUserProvider domainUserProvider) 
            : base(hmUnitOfWork, domainUserProvider)
        {
        }

        public virtual ActionResult Overview()
        {
            var vm = new HolidayOverviewViewModel
            {
                LoggedInUserName = _domainUserProvider.GetLoggedInUsername(),
                MonthlyCalendars = base.CreateMonthlyCalendarViewModels(CurrentEmployee),
                RequestViewModel = new RequestViewModel
                {
                    Purposes = new SelectList(_hmUnitOfWork.PurposeRepository.GetAll(), "Id", "Description"),
                }
            };

            return View(MVC.Holiday.Views.Overview, vm);
        }

        [HttpPost]
        public virtual ActionResult CreateRequest(RequestViewModel requestVM)
        {
            var request = new Request
            {
                EmployeeId = CurrentEmployee.Id,
                StartDate = requestVM.StartDate,
                EndDate = requestVM.EndDate,
                PurposeId = requestVM.PurposeId,
                RequestDate = DateTime.Now,
            };

            _hmUnitOfWork.RequestRepository.Add(request);

            _hmUnitOfWork.Save();

            var result = new
            {
                success = true,
            };

            return Json(result);
        }

        public virtual ActionResult GetRequests() 
        {
            var requests = _hmUnitOfWork.RequestRepository.FindBy(x => x.EmployeeId == CurrentEmployee.Id).ToList();

            var vm = new GridDataJson
            {
                records = requests.Count(),
                rows = requests.Select(x => 
                    new GridRowJson
                    { 
                        id = x.Id,
                        cell = new object[] 
                        {
                            x.StartDate.ToString("dd-MM-yyyy"), 
                            x.EndDate.ToString("dd-MM-yyyy"), 
                            x.Purpose.Description,
                            x.Accepted != null ? "accepted" : "pending"
                        }
                    }).ToList()
            };

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult EditRequest(String oper, int id)
        {
            if (oper == "del")
            {
                var request = _hmUnitOfWork.RequestRepository.GetSingle(id);

                if (request != null)
                {
                    _hmUnitOfWork.RequestRepository.Delete(request);

                    _hmUnitOfWork.Save();
                }
            }

            return Json(new {success = false});
        }
    }
}
