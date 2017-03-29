using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVSE.DAL.HolidayManagement.Entity
{
    public class Request : IEntity
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime RequestDate { get; set; }

        public String RequestNote { get; set; }

        public bool? Accepted { get; set; }

        public String RequestStateNote { get; set; }

        public DateTime? CancelDate { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public int? PurposeId { get; set; }

        public virtual Purpose Purpose { get; set; }

        public int? HolidayPeriodId { get; set; }

        public virtual HolidayPeriod HolidayPeriod { get; set; }
    }
}
