using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public class WorkerName
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string GetFullName()
        {
            return Name + " " + Surname;
        }
    }
}
