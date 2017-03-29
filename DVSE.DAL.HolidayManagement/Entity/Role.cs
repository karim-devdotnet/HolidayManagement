using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVSE.DAL.HolidayManagement.Entity
{
    public class Role : IEntity
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
