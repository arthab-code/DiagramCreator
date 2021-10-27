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
    /// <summary>
    /// Logika interakcji dla klasy ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            InitializeComponent();
        }

        private void AddWorkPlace(object sender, RoutedEventArgs e)
        {
            if (WorkPlace.Text.Length == 0)
            {
                MessageBox.Show("Wprowadź miejsce pracy!");
            } else
            {
                WorkPlacesManager workPlacesManager = new WorkPlacesManager();
                workPlacesManager.CreatePlace(WorkPlace.Text);
                MessageBox.Show("Prawidłowo dodano miejsce pracy : "+ WorkPlace.Text);
                this.Close();
            }
        }
    }
}
