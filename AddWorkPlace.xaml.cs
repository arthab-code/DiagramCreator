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
    /// Logika interakcji dla klasy AddWorkPlace.xaml
    /// </summary>
    public partial class AddWorkPlace : Window
    {
        public AddWorkPlace()
        {
            InitializeComponent();
        }

        private void AddPlace(object sender, RoutedEventArgs e)
        {
            WorkPlacesManager workPlacesManager = new WorkPlacesManager();
            workPlacesManager.CreatePlace(WorkPlace.Text);
            MessageBox.Show("Poprawnie dodano miejsce pracy o nazwie : " + WorkPlace.Text);
            this.Close();
        }
    }
}
