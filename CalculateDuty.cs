using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public class CalculateDuty
    {
        public void CalculateDriverDay(MonthlyDays monthlyDays, CalculatedMonthlyDays calculatedMonthlyDays, List<Worker> workers)
        {
            int hours = 0;
            foreach (var worker in workers)
            {
                hours += worker.DriverHoursDay;
            }
            Console.WriteLine(hours);
            calculatedMonthlyDays.DriverCalculatedDay = monthlyDays.DriverHoursDay - hours;
        }

        public void CalculateDriverNight(MonthlyDays monthlyDays, CalculatedMonthlyDays calculatedMonthlyDays, List<Worker> workers)
        {
            int hours = 0;
            foreach (var worker in workers)
            {
                hours += worker.DriverHoursNight;
            }
            calculatedMonthlyDays.DriverCalculatedNight = monthlyDays.DriverHoursNight - hours;

        }

        public void CalculateExecutiveDay(MonthlyDays monthlyDays, CalculatedMonthlyDays calculatedMonthlyDays, List<Worker> workers)
        {
            int hours = 0;
            foreach (var worker in workers)
            {
                hours += worker.ExecutiveHoursDay;
            }
            calculatedMonthlyDays.ExecutiveCalculatedDay = monthlyDays.ExecutiveHoursDay - hours;
        }

        public void CalculateExecutiveNight(MonthlyDays monthlyDays, CalculatedMonthlyDays calculatedMonthlyDays, List<Worker> workers)
        {
            int hours = 0;
            foreach (var worker in workers)
            {
                hours += worker.ExecutiveHoursNight;
            }
            calculatedMonthlyDays.ExecutiveCalculatedNight = monthlyDays.ExecutiveHoursNight - hours;
        }
    }
}
