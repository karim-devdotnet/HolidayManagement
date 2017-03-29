using DVSE.DAL.HolidayManagement.Entity;
using GenericRepository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVSE.DAL.HolidayManagement.EF.UnitOfWork
{
    public class HMUnitOfWork : IHMUnitOfWork
    {
        private IEntitiesContext _dbContext;

        private IEntityRepository<Employee> _employeeRepository;
        private IEntityRepository<HolidayInformation> _holidayInformationRepository;
        private IEntityRepository<HolidayPeriod> _holidayPeriodRepository;
        private IEntityRepository<LegalHoliday> _legalHolidayRepository;
        private IEntityRepository<Purpose> _purposeRepository;
        private IEntityRepository<Request> _requestRepository;
        private IEntityRepository<Role> _roleRepository;
        private IEntityRepository<Team> _teamRepository;

        public IEntityRepository<Employee> EmployeeRepository
        {
            get { return _employeeRepository; }
        }
        public IEntityRepository<HolidayInformation> HolidayInformationRepository
        {
            get { return _holidayInformationRepository; }
        }
        public IEntityRepository<HolidayPeriod> HolidayPeriodRepository
        {
            get { return _holidayPeriodRepository; }
        }
        public IEntityRepository<LegalHoliday> LegalHolidayRepository
        {
            get { return _legalHolidayRepository; }
        }
        public IEntityRepository<Purpose> PurposeRepository
        {
            get { return _purposeRepository; }
        }
        public IEntityRepository<Request> RequestRepository
        {
            get { return _requestRepository; }
        }
        public IEntityRepository<Role> RoleRepository
        {
            get { return _roleRepository; }
        }
        public IEntityRepository<Team> TeamRepository
        {
            get { return _teamRepository; }
        }

        public HMUnitOfWork(IEntitiesContext dbContext)
        {
            _dbContext = dbContext;

            _employeeRepository = new EntityRepository<Employee>(dbContext);
            _holidayInformationRepository = new EntityRepository<HolidayInformation>(dbContext);
            _holidayPeriodRepository = new EntityRepository<HolidayPeriod>(dbContext);
            _legalHolidayRepository = new EntityRepository<LegalHoliday>(dbContext);
            _purposeRepository = new EntityRepository<Purpose>(dbContext);
            _requestRepository = new EntityRepository<Request>(dbContext);
            _roleRepository = new EntityRepository<Role>(dbContext);
            _teamRepository = new EntityRepository<Team>(dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
