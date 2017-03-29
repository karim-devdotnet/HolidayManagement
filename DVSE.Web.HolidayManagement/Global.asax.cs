using DVSE.DAL.HolidayManagement.EF;
using DVSE.DAL.HolidayManagement.EF.UnitOfWork;
using DVSE.DAL.HolidayManagement.Entity;
using DVSE.Web.HolidayManagement.App_Start;
using DVSE.Web.HolidayManagement.Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DVSE.Web.HolidayManagement
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<HMContext>(null);

            var domainUserProvider = NinjectWebCommon.Kernel.GetService(typeof(IDomainUserProvider)) as IDomainUserProvider;
            var hmUnitOfWork = NinjectWebCommon.Kernel.GetService(typeof(IHMUnitOfWork)) as IHMUnitOfWork;

            var adminADName = ConfigurationManager.AppSettings["AdminADName"];

            var adminUserRole = hmUnitOfWork.RoleRepository.FindBy(x => x.Name == "AdminUser").SingleOrDefault();
            var normalUserRole = hmUnitOfWork.RoleRepository.FindBy(x => x.Name == "NormalUser").SingleOrDefault();

            foreach (var domainUser in domainUserProvider.GetAllUsers())
            { 
                var employee = hmUnitOfWork.EmployeeRepository.FindBy(x => x.ADName == domainUser.Name).SingleOrDefault();

                if (employee == null)
                {
                    employee = new Employee
                    {
                        FirstName = domainUser.Name,
                        LastName = domainUser.Name,
                        ADName = domainUser.Name,
                        EmailAddress = domainUser.EmailAddress,
                        RoleId = domainUser.Name == adminADName ? adminUserRole.Id : normalUserRole.Id
                    };

                    hmUnitOfWork.EmployeeRepository.Add(employee);

                    var holidayInformation = new HolidayInformation
                    {
                        DaysAvailable = 21,
                        Year = DateTime.Now.Year,
                        Employee = employee
                    };

                    hmUnitOfWork.HolidayInformationRepository.Add(holidayInformation);
                }
            }

            hmUnitOfWork.Save();
        }
    }
}