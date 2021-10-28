using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.IO;

namespace Grafik
{
    public class DutyDisplayer
    {
        public void RefreshDisplayer
            (CalculatedMonthlyDays calculatedMonthlyDays, 
            TextBlock general, 
            TextBlock driverDay, 
            TextBlock driverNight, 
            TextBlock executiveDay, 
            TextBlock executiveNight)
        {
            general.Text = calculatedMonthlyDays.MonthlyCalculated.ToString();
            driverDay.Text = calculatedMonthlyDays.DriverCalculatedDay.ToString();
            driverNight.Text = calculatedMonthlyDays.DriverCalculatedNight.ToString();
            executiveDay.Text = calculatedMonthlyDays.ExecutiveCalculatedDay.ToString();
            executiveNight.Text = calculatedMonthlyDays.ExecutiveCalculatedNight.ToString();
        }
    }
}
