using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Grafik
{
    public class WorkersFileDatabase : IWorkersDatabase
    {
        private Worker _worker;
        private string _pathDirectories = "AppData/Workers";
        private string _configFileName = "config.txt";
        private Paths _paths = new Paths();
        private bool _validateData = true;
        private WorkerName _workerName = new WorkerName();

        public string PathDirectories 
        { 
            get => _pathDirectories; 
            set => _pathDirectories = value; 
        }
        public string ConfigFileName
        { 
            get => _configFileName; 
            set => _configFileName = value;
        }

        public void CreateWorker(Worker worker)
        {
            _worker = worker;
            _workerName.Name = _worker.Name;
            _workerName.Surname = _worker.Surname;
            CreateWorkerDirectory();
            CreateWorkerDataFile();
            WriteConfigFile();
        }

        public void DeleteWorker(string name, string surname)
        {
            _workerName.Name = name;
            _workerName.Surname = surname;

            if (Directory.Exists(_paths._workersPath+"\\"+ _workerName.GetFullName()))
            {
                Directory.Delete(_paths._workersPath + "\\" + _workerName.GetFullName(), true);
                Console.WriteLine("Pomyslnie wyjebales chuja");
                return;
            }
            Console.WriteLine("NIE ISTNIEJE");
        }

        public Worker ReadWorker(string name, string surname)
        {
            var _workerName = new WorkerName();
            _workerName.Name = name;
            _workerName.Surname = surname;

            var workerPath = _paths._workersPath + "/" + _workerName.GetFullName() + "/" + _paths._workerDataFile;

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
                tempWorker.DriverHoursDay = int.Parse(streamReader.ReadLine());


                streamReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
            return tempWorker;
        }

        public void UpdateWorker(Worker worker)
        {
            _workerName.Name = worker.Name;
            _workerName.Surname = worker.Surname;
            _worker = worker;

            WriteConfigFile();
        }

        private void CreateWorkerDirectory()
        {
            DirectoryInfo dir = new DirectoryInfo(_paths._workersPath);

            /* Check exist worker in database */
            var foldersNames = dir.GetDirectories();

            foreach (var folderName in foldersNames)
            {
                if (folderName.Name == (_workerName.GetFullName()))
                {
                    _validateData = false;
                    Console.WriteLine("Pracownik o takim imieniu i nazwisku istnieje. Jeśli faktycznie posiadasz pracownikow o takich samych imionach i nazwiskach - zajeb jednego z nich");
                    return;
                }
            }

            dir.CreateSubdirectory(_workerName.GetFullName());
        }

        private void CreateWorkerDataFile()
        {
            if (_validateData == false)
                return;

            var workerPath = _paths._workersPath + "/" + _workerName.GetFullName() + "/" + _paths._workerDataFile;

            if (File.Exists(workerPath))
            {
                _validateData = false;
                Console.WriteLine("Taki plik juz istnieje ziom");
                return;
            }

            using (FileStream fs = File.Create(workerPath))
            {
                Console.WriteLine("CREATE FILE PATH : " + workerPath);

                fs.Close();
            }
        }

        private void WriteConfigFile()
        {
            if (_validateData == false)
                return;

            var workerPath = _paths._workersPath + "/" + _workerName.GetFullName() + "/" + _paths._workerDataFile;
            var name = _worker.Name;
            var surname = _worker.Surname;
            var workerPlaceName = _worker.WorkPlaceName;
            var workDaysPerMonth = _worker.WorkDaysPerMonth.ToString();
            int workTypeTemp = (int)_worker.WorkType;
            var workType = workTypeTemp.ToString();
            int agreementTypeTemp = (int)_worker.AgreementType;
            var agreementType = agreementTypeTemp.ToString();
            var driverHoursDay = _worker.DriverHoursDay.ToString();

            using (StreamWriter streamWriter = new StreamWriter(workerPath))
            {
                streamWriter.WriteLine(name);
                streamWriter.WriteLine(surname);
                streamWriter.WriteLine(workerPlaceName);
                streamWriter.WriteLine(workDaysPerMonth);
                streamWriter.WriteLine(workType);
                streamWriter.WriteLine(agreementType);
                streamWriter.WriteLine(driverHoursDay);

                streamWriter.Close();
            }

            Console.WriteLine("Write sucessfull worker");
        }
    }

}
