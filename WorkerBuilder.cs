using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public class WorkerBuilder : IWorkerBuilder
    {
        private Worker _worker = new Worker();
        public Worker Build()
        {
            _worker.WorkDaysPerMonth = _worker.DriverDutyDay + _worker.DriverDutyNight + _worker.ExecutiveDutyDay + _worker.ExecutiveDutyNight;
            return _worker;
        }

        public IWorkerBuilder SetName(string name)
        {
            _worker.Name = name;
            return this;
        }

        public IWorkerBuilder SetSurname(string surname)
        {
            _worker.Surname = surname;
            return this;
        }

        public IWorkerBuilder SetWorkDaysPerMonth(int workDaysPerMonth)
        {
            _worker.WorkDaysPerMonth = workDaysPerMonth;
            return this;
        }

        public IWorkerBuilder SetDriverHoursDay(int driverHoursDay)
        {
            _worker.DriverDutyDay = driverHoursDay;
            return this;
        }

        public IWorkerBuilder SetDriverHoursNight(int driverHoursNight)
        {
            _worker.DriverDutyNight = driverHoursNight;
            return this;
        }
        public IWorkerBuilder SetExecutiveHoursDay(int executiveHoursDay)
        {
            _worker.ExecutiveDutyDay = executiveHoursDay;
            return this;
        }
        public IWorkerBuilder SetExecutiveHoursNight(int executiveHoursNight)
        {
            _worker.ExecutiveDutyNight = executiveHoursNight;
            return this;
        }

        public IWorkerBuilder SetWorkPlaceName(string workPlaceName)
        {
            _worker.WorkPlaceName = workPlaceName;
            return this;
        }

        public IWorkerBuilder SetWorkType(WorkType workType)
        {
            _worker.WorkType = workType;
            return this;
        }

        public IWorkerBuilder SetAgreementType(AgreementType agreementType)
        {
            _worker.AgreementType = agreementType;
            return this;
        }

        public IWorkerBuilder SetWorkSystem(WorkSystem workSystem)
        {
            _worker.WorkSystem = workSystem;
            return this;
        }
    }
}
