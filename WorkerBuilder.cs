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
    }
}
