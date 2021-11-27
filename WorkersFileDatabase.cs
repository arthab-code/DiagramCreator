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
                tempWorker.DriverDutyDay = int.Parse(streamReader.ReadLine());
                tempWorker.DriverDutyNight = int.Parse(streamReader.ReadLine());
                tempWorker.ExecutiveDutyDay = int.Parse(streamReader.ReadLine());
                tempWorker.ExecutiveDutyNight = int.Parse(streamReader.ReadLine());

                streamReader.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var read = File.ReadAllLines(freeTimePath);

            for (int i=0; i < read.Length; i++)
            {
                tempWorker.FreeDays[i] = char.Parse(read[i]);

                if (read[i] == "x")
                {
                    tempWorker.FreeDaysDisplay += (i+1).ToString() + ",";
                    continue;
                }
            }
            if (tempWorker.FreeDaysDisplay != null ) // TU JEST BLAD -> LICZBY DWUCYFROWE BEDA UCINANE O 1
            {
                if (tempWorker.FreeDaysDisplay.Length > 2 && tempWorker.FreeDaysDisplay[1] != ',')
                {
                    var removeLastChar = tempWorker.FreeDaysDisplay.Remove(tempWorker.FreeDaysDisplay.Length - 1, 1);
                    tempWorker.FreeDaysDisplay = removeLastChar;
                }
            }

            /* Workers information enum parse to string displayer */
            if (tempWorker.WorkType == WorkType.Hybrid)
                tempWorker.WorkTypeDisplayer = "Ratownik-Kierowca";
            else if (tempWorker.WorkType == WorkType.Driver)
                tempWorker.WorkTypeDisplayer = "Kierowca";
            else if (tempWorker.WorkType == WorkType.Executive)
                tempWorker.WorkTypeDisplayer = "Kierownik ZRM";

            if (tempWorker.WorkSystem == WorkSystem.FullDuty)
                tempWorker.WorkSystemDisplayer = "24h";
            else if (tempWorker.WorkSystem == WorkSystem.HalfDuty)
                tempWorker.WorkSystemDisplayer = "12h";

            if (tempWorker.AgreementType == AgreementType.Permanent)
                tempWorker.WorkAgreementDisplayer = "Etat";
            else if (tempWorker.AgreementType == AgreementType.Other)
                tempWorker.WorkAgreementDisplayer = "Kontrakt";
            /* Workers information enum parse to string displayer -- end */
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
            var driverHoursDay = _worker.DriverDutyDay.ToString();
            var driverHoursNight = _worker.DriverDutyNight.ToString();
            var executiveHoursDay = _worker.ExecutiveDutyDay.ToString();
            var executiveHoursNight = _worker.ExecutiveDutyNight.ToString();

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
                for(int i = 0; i < _worker.FreeDays.Length; i++)
                    streamWriter.WriteLine(_worker.FreeDays[i].ToString());

                streamWriter.Close();
            }
            Console.WriteLine("Write sucessfull worker");
        }
    }

}
