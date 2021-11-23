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
        private char[]_freeDays;
        private int _workDaysPerMonth;
        private int _driverHoursDay;
        private int _executiveHoursDay;
        private int _driverHoursNight;
        private int _executiveHoursNight;
        private WorkType _workType;
        private AgreementType _agreementType;
        private WorkSystem _workSystem;
        private char[] _workDiagramDay;
        private char[] _workDiagramNight;
        private string[] _diagramDisplayer;
        private string _freeDaysDisplay;

        public Worker()
        {
            _freeDays = new char[32];
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

        public char[] FreeDays
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

        public int DriverDutyDay 
        { 
            get => _driverHoursDay; 
            set => _driverHoursDay = value; 
        }
        public int ExecutiveDutyDay 
        { 
            get => _executiveHoursDay; 
            set => _executiveHoursDay = value; 
        }
        public int DriverDutyNight 
        { 
            get => _driverHoursNight; 
            set => _driverHoursNight = value; 
        }
        public int ExecutiveDutyNight 
        {
            get => _executiveHoursNight;
            set => _executiveHoursNight = value; 
        }
        public AgreementType AgreementType 
        { 
            get => _agreementType; 
            set => _agreementType = value; 
        }
        public WorkSystem WorkSystem 
        { 
            get => _workSystem; 
            set => _workSystem = value; 
        }
        public string FreeDaysDisplay 
        { 
            get => _freeDaysDisplay; 
            set => _freeDaysDisplay = value; 
        }
        public char[] WorkDiagramDay { get => _workDiagramDay; set => _workDiagramDay = value; }
        public char[] WorkDiagramNight { get => _workDiagramNight; set => _workDiagramNight = value; }
        public string[] DiagramDisplayer { get => _diagramDisplayer; set => _diagramDisplayer = value; }
    }
}
