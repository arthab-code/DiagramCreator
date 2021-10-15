using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public class Worker
    {
        private string _name;
        private string _surname;
        private string _workPlaceName;
        private List<byte>_freeDays;
        private int _workDaysPerMonth;
        private int _driverHoursDay;
        private int _executiveHoursDay;
        private int _driverHoursNight;
        private int _executiveHoursNight;
        private WorkType _workType;
        private AgreementType _agreementType;
        private char[,] _workDiagram;

        public Worker()
        {
            _freeDays = new List<byte>();
        }

        public string Name
        {
            get =>  _name; 
            set => _name = value; 
        }

        public string Surname
        {
            get => _surname; 
            set => _surname = value; 
        }

        public string WorkPlaceName
        {
            get => _workPlaceName; 
            set => _workPlaceName = value; 
        }

        public List<byte> FreeDays
        {
            get => _freeDays; 
            set => _freeDays = value; 
        }

        public int WorkDaysPerMonth
        {
            get => _workDaysPerMonth; 
            set => _workDaysPerMonth = value; 
        }

        public WorkType WorkType
        {
            get => _workType; 
            set => _workType = value; 
        }

        public int DriverHoursDay 
        { 
            get => _driverHoursDay; 
            set => _driverHoursDay = value; 
        }
        public int ExecutiveHoursDay 
        { 
            get => _executiveHoursDay; 
            set => _executiveHoursDay = value; 
        }
        public int DriverHoursNight 
        { 
            get => _driverHoursNight; 
            set => _driverHoursNight = value; 
        }
        public int ExecutiveHoursNight 
        {
            get => _executiveHoursNight;
            set => _executiveHoursNight = value; 
        }
        public AgreementType AgreementType 
        { 
            get => _agreementType; 
            set => _agreementType = value; 
        }
        public char[,] WorkDiagram 
        { 
            get => _workDiagram; 
            set => _workDiagram = value; 
        }
    }
}
