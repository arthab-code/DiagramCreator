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
            private set { }
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

        public void CalculateWorkDays(int month, int year)
        {
            var calculatedDays = DateTime.DaysInMonth(year, month);

            _workDaysInMonth = calculatedDays * 2;
            _driverHoursDay = calculatedDays;
            _executiveHoursDay = calculatedDays;
            _driverHoursNight = calculatedDays;
            _executiveHoursNight = calculatedDays;
        }
    }
}
