using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Grafik
{
    public class MonthlyDays
    {
        private int _workDaysInMonth;
        public int WorkDaysInMonth
        {
            get => _workDaysInMonth;
            private set { }
        }

        public void CalculateWorkDays(int month, int year)
        {
            var calculatedDays = DateTime.DaysInMonth(year, month);

            _workDaysInMonth = calculatedDays * 2;
        }
    }
}
