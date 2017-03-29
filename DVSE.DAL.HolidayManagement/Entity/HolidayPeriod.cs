using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVSE.DAL.HolidayManagement.Entity
{
    public class HolidayPeriod : IEntity
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public String Note { get; set; }

        public DateTime? CancelDate { get; set; }

        public bool? Cancelled { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public int? RequestId { get; set; }

        public virtual Request Request { get; set; }

        public int PurposeId { get; set; }

        public virtual Purpose Purpose { get; set; }

        public int? LegalHolidayId { get; set; }

        public virtual LegalHoliday LegalHoliday { get; set; }
    }
}
