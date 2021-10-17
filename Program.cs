using System;
using System.Collections.Generic;
using System.Linq;

namespace Grafik
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker worker = new WorkerBuilder()
                 .SetName("Artur")
                 .SetSurname("Habrajski")
                 .SetWorkDaysPerMonth(18)
                 .SetWorkPlaceName("s0450")
                 .SetWorkType(WorkType.Hybrid)
                 .SetAgreementType(AgreementType.Permanent)
                 .Build();
            Worker worker2 = new WorkerBuilder()
     .SetName("Artur43434")
     .SetSurname("Habrajski34343")
     .SetWorkDaysPerMonth(18)
     .SetWorkPlaceName("s0450")
     .SetWorkType(WorkType.Hybrid)
     .SetAgreementType(AgreementType.Permanent)
     .Build();
            Worker worker3 = new WorkerBuilder()
     .SetName("Arddftur")
     .SetSurname("Habrajsfdfdsski")
     .SetWorkDaysPerMonth(18)
     .SetWorkPlaceName("s0450")
     .SetWorkType(WorkType.Hybrid)
     .SetAgreementType(AgreementType.Permanent)
     .Build();

            var saveSystem = new WorkersFileDatabase();
            saveSystem.CreateWorker(worker);
            saveSystem.CreateWorker(worker2);
            saveSystem.CreateWorker(worker3);
            WorkersManager _workersManager = new WorkersManager();
            _workersManager.LoadWorkersToList();

            foreach(var w in _workersManager.Workers)
            {
               // Console.WriteLine(w.Name);
                ShowData(w);
                Console.WriteLine();
            }

            IWorkPlacesDatabase savePlace = new WorkPlacesFileDatabase();
            savePlace.CreateWorkPlace("s0450");
            savePlace.CreateWorkPlace("s0415");

            List<string> directories = new List<string>();

            directories = savePlace.ReadWorkPlaces();

            foreach(var a in directories)
            {
                Console.WriteLine(a);
            }

        }

        public static void ShowData(Worker worker)
        {
            Console.WriteLine(worker.Name);
            Console.WriteLine(worker.Surname);
            Console.WriteLine(worker.WorkDaysPerMonth);
            Console.WriteLine(worker.WorkType);
            Console.WriteLine(worker.AgreementType);
            Console.WriteLine(worker.WorkPlaceName);
        }
    }
}
