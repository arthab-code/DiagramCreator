using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Grafik
{
    public class FileSystemSave : ISaveWorker
    {
        private Worker _worker;
        private string _pathDirectories = "AppData/Workers";
        private string _configFileName = "config.txt";
        private bool _validateData = true;

        public void SetWorker(Worker worker)
        {
            _worker = worker;
        }

        public void SaveWorkerToDatabase()
        {
            CreateWorkerDirectory();
            CreateWorkerDataFile();
            WriteConfigFile();
        }

        private void CreateWorkerDirectory()
        {
            DirectoryInfo dir = new DirectoryInfo(_pathDirectories);

            /* Check exist worker in database */
            var foldersNames = dir.GetDirectories();
            
            foreach(var folderName in foldersNames)
            {
                if (folderName.Name == (_worker.Name+_worker.Surname))
                {
                    _validateData = false;
                    Console.WriteLine("Pracownik o takim imieniu i nazwisku istnieje. Jeśli faktycznie posiadasz pracownikow o takich samych imionach i nazwiskach - zajeb jednego z nich");
                    return;
                }
            }

            dir.CreateSubdirectory(_worker.Name + _worker.Surname);
        }

        private void CreateWorkerDataFile()
        {
            if (_validateData == false)
                return;

            var workerPath = _pathDirectories +"/"+ _worker.Name + _worker.Surname + "/"+_configFileName;

            if (File.Exists(workerPath))
            {
                _validateData = false;
                Console.WriteLine("Taki plik juz istnieje ziom");
                return;
            }

            using (FileStream fs = File.Create(workerPath))
            {
                Console.WriteLine("CREATE FILE PATH : " +workerPath);
            }
        }

        private void WriteConfigFile()
        {
            if (_validateData == false)
                return;

            var workerPath = _pathDirectories + "/" + _worker.Name + _worker.Surname + "/" + _configFileName;
            var name = _worker.Name;
            var surname = _worker.Surname;
            var workerPlaceName = _worker.WorkPlaceName;
            var workDaysPerMonth = _worker.WorkDaysPerMonth.ToString();
            int workTypeTemp = (int)_worker.WorkType;
            var workType = workTypeTemp.ToString();
            var freeDays = "null";

            using (StreamWriter streamWriter = new StreamWriter(workerPath))
            {
                streamWriter.WriteLine(name);
                streamWriter.WriteLine(surname);
                streamWriter.WriteLine(workerPlaceName);
                streamWriter.WriteLine(workDaysPerMonth);
                streamWriter.WriteLine(workType);
                streamWriter.Write(freeDays);
            }

            Console.WriteLine("Write sucessfull worker");
        }
    }
}
