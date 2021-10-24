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
        public void Generate(MonthlyDays monthlyDays, int year, int month)
        {
            monthlyDays.WorkDaysInMonth = DateTime.DaysInMonth(year, month);
            monthlyDays.ExecutiveHoursDay = monthlyDays.WorkDaysInMonth;
            monthlyDays.ExecutiveHoursNight = monthlyDays.WorkDaysInMonth;
            monthlyDays.DriverHoursDay = monthlyDays.WorkDaysInMonth;
            monthlyDays.DriverHoursNight = monthlyDays.WorkDaysInMonth;
        }
    }
}
