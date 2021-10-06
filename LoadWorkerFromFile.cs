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

        public Worker LoadWorkerData(string name, string surname)
        {
            var workerPath = _pathDirectories + "/" + name + surname + "/" + _configFileName;

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
                tempWorker.FreeDays = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return tempWorker;
        }
    }
}
