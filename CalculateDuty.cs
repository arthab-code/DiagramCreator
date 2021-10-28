using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public class CalculateDuty
    {
        public void CalculateDriverDay(MonthlyDays monthlyDays, CalculatedMonthlyDays calculatedMonthlyDays, List<Worker> workers, string workPlace)
        {
            int duty = 0;
            foreach (var worker in workers)
            {
                if (worker.WorkPlaceName == workPlace)
                    duty += worker.DriverHoursDay;
            }
            Console.WriteLine(duty);
            calculatedMonthlyDays.DriverCalculatedDay = monthlyDays.DriverHoursDay - duty;
        }

        public void CalculateDriverNight(MonthlyDays monthlyDays, CalculatedMonthlyDays calculatedMonthlyDays, List<Worker> workers, string workPlace)
        {
            int duty = 0;
            foreach (var worker in workers)
            {
                if (worker.WorkPlaceName == workPlace)
                    duty += worker.DriverHoursNight;
            }
            calculatedMonthlyDays.DriverCalculatedNight = monthlyDays.DriverHoursNight - duty;

        }

        public void CalculateExecutiveDay(MonthlyDays monthlyDays, CalculatedMonthlyDays calculatedMonthlyDays, List<Worker> workers, string workPlace)
        {
            int duty = 0;
            foreach (var worker in workers)
            {
                if (worker.WorkPlaceName == workPlace)
                    duty += worker.ExecutiveHoursDay;
            }
            calculatedMonthlyDays.ExecutiveCalculatedDay = monthlyDays.ExecutiveHoursDay - duty;
        }

        public void CalculateExecutiveNight(MonthlyDays monthlyDays, CalculatedMonthlyDays calculatedMonthlyDays, List<Worker> workers, string workPlace)
        {
            int duty = 0;
            foreach (var worker in workers)
            {
                if (worker.WorkPlaceName == workPlace)
                    duty += worker.ExecutiveHoursNight;
            }
            calculatedMonthlyDays.ExecutiveCalculatedNight = monthlyDays.ExecutiveHoursNight - duty;
        }

        public void CalculateMonthlyDays(MonthlyDays monthlyDays, CalculatedMonthlyDays calculatedMonthlyDays)
        {
            calculatedMonthlyDays.MonthlyCalculated = 
                + calculatedMonthlyDays.DriverCalculatedDay 
                + calculatedMonthlyDays.DriverCalculatedNight 
                + calculatedMonthlyDays.ExecutiveCalculatedDay 
                + calculatedMonthlyDays.ExecutiveCalculatedNight;
        }
    }
}
