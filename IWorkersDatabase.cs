using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public interface IWorkersDatabase
    {
        void CreateWorker(Worker worker);
        Worker ReadWorker(string name, string surname);
        void UpdateWorker(Worker worker);
        void DeleteWorker(string name, string surname);
    }
}
