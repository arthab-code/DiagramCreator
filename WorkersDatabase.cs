using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public class WorkersDatabase
    {
        private ILoadWorker _workerLoader = new LoadWorkerFromFile();
        private List<Worker> _workers = new List<Worker>();

        public List<Worker> Workers
        {
            get => _workers;
            set => _workers = value;
        }

        public void LoadWorkersToList()
        {
            _workers = _workerLoader.LoadAllWorkers();
        }
    }
}
