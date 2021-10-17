using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Grafik
{
    public class LoadWorkerFromFile : ILoadWorker
    {
        private string _pathDirectories = "AppData/Workers";
        private string _configFileName = "config.txt";

        private Worker LoadWorkerData(string name, string surname)
        {
            var _workerName = new WorkerName();
            _workerName.Name = name;
            _workerName.Surname = surname;

            var workerPath = _pathDirectories + "/" + _workerName.GetFullName() + "/" + _configFileName;

            Worker tempWorker = new Worker();
            
            try
            {
                StreamReader streamReader = new StreamReader(workerPath);

                tempWorker.Name = streamReader.ReadLine();
                tempWorker.Surname = streamReader.ReadLine();
                tempWorker.WorkPlaceName = streamReader.ReadLine();
                tempWorker.WorkDaysPerMonth = int.Parse(streamReader.ReadLine());
                int workTypeTemp = int.Parse(streamReader.ReadLine());
                tempWorker.WorkType = (WorkType)workTypeTemp;
                int agreementTypeTemp = int.Parse(streamReader.ReadLine());
                tempWorker.AgreementType = (AgreementType)agreementTypeTemp;
                tempWorker.FreeDays = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return tempWorker;
        }

        public List<Worker> LoadAllWorkers()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_pathDirectories);

            var directories = directoryInfo.GetDirectories();

            var workersList = new List<Worker>();

            foreach (var directory in directories)
            {
                var nameTemp = directory.Name.Split(' ');

                workersList.Add(LoadWorkerData(nameTemp[0], nameTemp[1]));
            }

            return workersList;
        }
    }
}
