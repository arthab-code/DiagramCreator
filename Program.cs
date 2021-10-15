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

            var saveSystem = new FileWorkerSave();
            saveSystem.SetWorker(worker);
            saveSystem.SaveWorkerToDatabase();
            saveSystem.SetWorker(worker2);
            saveSystem.SaveWorkerToDatabase();
            saveSystem.SetWorker(worker3);
            saveSystem.SaveWorkerToDatabase();
            WorkersDatabase _workersDatabase = new WorkersDatabase();
            _workersDatabase.LoadWorkersToList();

            foreach(var w in _workersDatabase.Workers)
            {
               // Console.WriteLine(w.Name);
                ShowData(w);
                Console.WriteLine();
            }

            ISaveWorkPlace savePlace = new FileWorkPlaceSave();
            savePlace.SaveWorkPlaceToDatabase("s0450");
            savePlace.SaveWorkPlaceToDatabase("s0415");
            ILoadWorkPlace loadWorkPlace = new LoadWorkPlaceFromFile();

            List<string> directories = new List<string>();

            directories = loadWorkPlace.LoadAllPlaces();

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
