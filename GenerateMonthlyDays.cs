using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Grafik
{
    public class GenerateMonthlyDays
    {
        private MonthlyDays _monthlyDays;
        public void Generate(MonthlyDays monthlyDays, int year, int month)
        {
            _monthlyDays = monthlyDays;

            _monthlyDays.WorkDaysInMonth = DateTime.DaysInMonth(year, month);
            _monthlyDays.ExecutiveHoursDay = _monthlyDays.WorkDaysInMonth;
            _monthlyDays.ExecutiveHoursNight = _monthlyDays.WorkDaysInMonth;
            _monthlyDays.DriverHoursDay = _monthlyDays.WorkDaysInMonth;
            _monthlyDays.DriverHoursNight = _monthlyDays.WorkDaysInMonth;
        }
    }
}
