using DVSE.DAL.HolidayManagement.Entity;
using GenericRepository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVSE.DAL.HolidayManagement.EF.UnitOfWork
{
    public interface IHMUnitOfWork
    {
        IEntityRepository<Employee> EmployeeRepository { get; }

        IEntityRepository<HolidayInformation> HolidayInformationRepository { get; }

        IEntityRepository<HolidayPeriod> HolidayPeriodRepository { get; }

        IEntityRepository<LegalHoliday> LegalHolidayRepository { get; }

        IEntityRepository<Purpose> PurposeRepository { get; }

        IEntityRepository<Request> RequestRepository { get; }

        IEntityRepository<Role> RoleRepository { get; }

        IEntityRepository<Team> TeamRepository { get; }

        void Save ();
    }
}
