using System;

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
                 .Build();

            var saveSystem = new FileSystemSave();
            saveSystem.SetWorker(worker);
            saveSystem.SaveWorkerToDatabase();
            LoadWorkerFromFile loadFromFile = new LoadWorkerFromFile();
            Worker tempWorker = loadFromFile.LoadWorkerData("Artur", "Habrajski");
            ShowData(tempWorker);
        }

        public static void ShowData(Worker worker)
        {
            Console.WriteLine(worker.Name);
            Console.WriteLine(worker.Surname);
            Console.WriteLine(worker.WorkDaysPerMonth);
            Console.WriteLine(worker.WorkType);
            Console.WriteLine(worker.WorkPlaceName);
        }
    }
}
