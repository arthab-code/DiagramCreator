using System;


namespace Grafik
{
    public class MonthlyDays
    {
        private int _workDaysInMonth;
        private int _executiveHoursDay;
        private int _driverHoursDay;
        private int _executiveHoursNight;
        private int _driverHoursNight;
        public int WorkDaysInMonth
        {
            get => _workDaysInMonth;
            set => _workDaysInMonth = value;
        }

        public int ExecutiveHoursDay
        {
            get => _executiveHoursDay;
            set => _executiveHoursDay = value; 
        }

        public int DriverHoursDay
        {
            get => _driverHoursDay;
            set => _driverHoursDay = value; 
        }

        public int ExecutiveHoursNight
        {
            get => _executiveHoursNight;
            set => _executiveHoursNight = value; 
        }

        public int DriverHoursNight
        {
            get => _driverHoursNight;
            set => _driverHoursNight = value; 
        }

    }
}
