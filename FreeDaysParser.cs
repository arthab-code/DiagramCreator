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
using System.Windows.Shapes;

namespace Grafik
{
    public static class FreeDaysParser
    {
        public static bool ParseFreeDays(List<byte> freeDays, string freeDaysFromTextBox )
        {
            freeDays.Clear();

            var freeDaysString = freeDaysFromTextBox;

            if (freeDaysString.Length == 0)
                return true;

            if (freeDaysString.Length == 1)
            {
                freeDays.Add(byte.Parse(freeDaysString));
                return true;
            }

            string[] freeDaysStringArray;

            try
            {
                freeDaysStringArray = freeDaysString.Split(',');
                for (int i = 0; i < freeDaysStringArray.Length; i++)
                {
                    freeDays.Add(byte.Parse(freeDaysStringArray[i]));
                }

                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + "Liczby w polu DNI WOLNE powinny być oddzielone od siebie znakiem ','  !!");

                return false;
            }
        }
    }
}
