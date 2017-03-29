using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVSE.DAL.HolidayManagement.Entity
{
    public class Employee : IEntity
    {
        public int Id { get; set; }

        public String ADName { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String EmailAddress { get; set; }

        public DateTime? HireDate { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<HolidayInformation> HolidayInformations { get; set; }

        public int? TeamId { get; set; }

        public virtual Team Team { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

        public virtual ICollection<HolidayPeriod> HolidayPeriods { get; set; }
    }
}
