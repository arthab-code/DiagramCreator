using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Grafik
{
    public class WorkersManager
    {
        private IWorkersDatabase _workersDatabase = new WorkersFileDatabase();
        private List<Worker> _workers = new List<Worker>();
        private Paths _paths = new Paths();

        public WorkersManager()
        {
            _workers = new List<Worker>();
        }

        public List<Worker> Workers
        {
            get => _workers;
            set => _workers = value;
        }

        public void LoadWorkersToList()
        {
            /* Clear list */
            _workers.Clear();

            DirectoryInfo directoryInfo = new DirectoryInfo(_paths._workersPath);

            var directories = directoryInfo.GetDirectories();

            foreach (var directory in directories)
            {
                var nameTemp = directory.Name.Split(' ');

                _workers.Add(_workersDatabase.ReadWorker(nameTemp[0], nameTemp[1]));
            }
        }

        public void Create(Worker worker)
        {
            _workersDatabase.CreateWorker(worker);
        }

        public void DeleteWorker(string name, string surname)
        {
            _workersDatabase.DeleteWorker(name, surname);
            LoadWorkersToList();
        }

        public void UpdateWorker(Worker worker)
        {
            _workersDatabase.UpdateWorker(worker);
            LoadWorkersToList();
        }
    } 
}
