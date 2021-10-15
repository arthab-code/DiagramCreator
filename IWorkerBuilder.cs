using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public interface IWorkerBuilder
    {
        IWorkerBuilder SetName(string name);
        IWorkerBuilder SetSurname(string surname);
        IWorkerBuilder SetWorkPlaceName(string workPlaceName);
        IWorkerBuilder SetWorkDaysPerMonth(int workDaysPerMonth);
        IWorkerBuilder SetWorkType(WorkType workType);
        IWorkerBuilder SetAgreementType(AgreementType agreementType);
        Worker Build();

    }
}
