using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DVSE.Web.HolidayManagement.Infrastructure.Authentication
{
    public class FakeDomainUserProvider : IDomainUserProvider
    {
        public IEnumerable<DomainUser> GetAllUsers()
        {
            var users = new List<DomainUser>();

            var adminADName =  ConfigurationManager.AppSettings["AdminADName"];

            users.Add(new DomainUser { Name = adminADName, EmailAddress = adminADName + "@address.com" });

            Enumerable
                .Range(1, 10)
                .ToList()
                .ForEach(x => users.Add(new DomainUser 
                    { 
                        Name = "Name" + x, 
                        EmailAddress = "Email" + x + "@address.com" 
                    }));

            return users;
        }


        public string GetLoggedInUsername()
        {
            return ConfigurationManager.AppSettings["FakeLoggedInUserADName"];
        }
    }
}