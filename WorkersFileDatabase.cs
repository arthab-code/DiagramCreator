using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Threading.Tasks;

namespace Grafik
{
    public class WorkersFileDatabase : IWorkersDatabase
    {
        private Worker _worker;
        private Paths _paths = new Paths();
        private bool _validateData = true;
        private WorkerName _workerName = new WorkerName();

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
                return;
            }
            MessageBox.Show("Nie istnieje taki pracownik " + name + " " + surname);
        }

        public Worker ReadWorker(string name, string surname)
        {
            var _workerName = new WorkerName();
            _workerName.Name = name;
            _workerName.Surname = surname;

            var workerPath = _paths._workersPath + "/" + _workerName.GetFullName() + "/" + _paths._workerDataFile;
            var freeTimePath = _paths._workersPath + "/" + _workerName.GetFullName() + "/" + _paths._workerFreeTime;

            Worker tempWorker = new Worker();

            try
            {
                StreamReader streamReader = new StreamReader(workerPath);

                tempWorker.Name = streamReader.ReadLine();
                tempWorker.Surname = streamReader.ReadLine();
                int agreementTypeTemp = int.Parse(streamReader.ReadLine());
                tempWorker.AgreementType = (AgreementType)agreementTypeTemp;
                int workSystemTemp = int.Parse(streamReader.ReadLine());
                tempWorker.WorkSystem = (WorkSystem)workSystemTemp;
                int workTypeTemp = int.Parse(streamReader.ReadLine());
                tempWorker.WorkType = (WorkType)workTypeTemp;
                tempWorker.WorkPlaceName = streamReader.ReadLine();
                tempWorker.DriverHoursDay = int.Parse(streamReader.ReadLine());
                tempWorker.DriverHoursNight = int.Parse(streamReader.ReadLine());
                tempWorker.ExecutiveHoursDay = int.Parse(streamReader.ReadLine());
                tempWorker.ExecutiveHoursNight = int.Parse(streamReader.ReadLine());

                /*
                 *                 streamWriter.WriteLine(name);
                streamWriter.WriteLine(surname);
                streamWriter.WriteLine(agreementType);
                streamWriter.WriteLine(workSystem);
                streamWriter.WriteLine(workType);
                streamWriter.WriteLine(workerPlaceName);          
                streamWriter.WriteLine(driverHoursDay);
                streamWriter.WriteLine(driverHoursNight);
                streamWriter.WriteLine(executiveHoursDay);
                streamWriter.WriteLine(executiveHoursNight);
                 */

                streamReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var read = File.ReadAllLines(freeTimePath);

            foreach (var item in read)
            {
                tempWorker.FreeDaysDisplay += item + " ";
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
            int workTypeTemp = (int)_worker.WorkType;
            var workType = workTypeTemp.ToString();
            int agreementTypeTemp = (int)_worker.AgreementType;
            var agreementType = agreementTypeTemp.ToString();
            int workSystemTemp = (int)_worker.WorkSystem;
            var workSystem = workSystemTemp.ToString();
            var driverHoursDay = _worker.DriverHoursDay.ToString();
            var driverHoursNight = _worker.DriverHoursNight.ToString();
            var executiveHoursDay = _worker.ExecutiveHoursDay.ToString();
            var executiveHoursNight = _worker.ExecutiveHoursNight.ToString();

            using (StreamWriter streamWriter = new StreamWriter(workerPath))
            {
                streamWriter.WriteLine(name);
                streamWriter.WriteLine(surname);
                streamWriter.WriteLine(agreementType);
                streamWriter.WriteLine(workSystem);
                streamWriter.WriteLine(workType);
                streamWriter.WriteLine(workerPlaceName);          
                streamWriter.WriteLine(driverHoursDay);
                streamWriter.WriteLine(driverHoursNight);
                streamWriter.WriteLine(executiveHoursDay);
                streamWriter.WriteLine(executiveHoursNight);

                streamWriter.Close();
            }

            var freeTimePath = _paths._workersPath + "/" + _workerName.GetFullName() + "/" + _paths._workerFreeTime;

            using (StreamWriter streamWriter = new StreamWriter(freeTimePath))
            {
                foreach (var item in _worker.FreeDays)
                    streamWriter.WriteLine(item.ToString());

                streamWriter.Close();
            }
            Console.WriteLine("Write sucessfull worker");
        }
    }

}
