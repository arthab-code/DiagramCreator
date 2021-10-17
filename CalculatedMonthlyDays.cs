using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public class CalculatedMonthlyDays
    {
        private int _driverCalculatedDay;
        private int _executiveCalculatedDay;
        private int _driverCalculatedNight;
        private int _executiveCalculatedNight;

        public int DriverCalculatedDay { get => _driverCalculatedDay; set => _driverCalculatedDay = value; }
        public int ExecutiveCalculatedDay { get => _executiveCalculatedDay; set => _executiveCalculatedDay = value; }
        public int DriverCalculatedNight { get => _driverCalculatedNight; set => _driverCalculatedNight = value; }
        public int ExecutiveCalculatedNight { get => _executiveCalculatedNight; set => _executiveCalculatedNight = value; }
    }
}
