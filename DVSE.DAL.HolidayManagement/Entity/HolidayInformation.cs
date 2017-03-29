using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVSE.DAL.HolidayManagement.Entity
{
    public class HolidayInformation : IEntity
    {
        public int Id { get; set; }

        public int DaysAvailable { get; set; }

        public int Year { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
