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
        public static bool ParseFreeDays(char[] freeDays, string freeDaysFromTextBox )
        {
            /* clear freeDays array */
            for (int i = 0; i < freeDays.Length; i++)
            {
                freeDays[i] = 'n';
            }

            var freeDaysString = freeDaysFromTextBox;

            if (freeDaysString.Length == 0)
                return true;

            /* Add single free day to char array */
            if (freeDaysString.Length == 1)
            {
                freeDays[byte.Parse(freeDaysString)] = 'x';
                return true;
            }

            string[] freeDaysStringArray;

            try
            {
                freeDaysStringArray = freeDaysString.Split(',');

                for (int i = 0; i < freeDays.Length; i++)
                {
                    for (int j = 0; j < freeDaysStringArray.Length; j++)
                    {
                        if (i == int.Parse(freeDaysStringArray[j]))
                        {
                            freeDays[i-1] = 'x';
                            continue;
                        }
                    }

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
