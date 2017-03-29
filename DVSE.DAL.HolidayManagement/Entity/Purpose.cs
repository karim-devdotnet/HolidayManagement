using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVSE.DAL.HolidayManagement.Entity
{
    public class Purpose : IEntity
    {
        public int Id { get; set; }

        public String Description { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

        public virtual ICollection<HolidayPeriod> HolidayPeriods { get; set; }
    }
}
