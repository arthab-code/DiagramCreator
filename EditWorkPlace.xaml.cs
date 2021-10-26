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
    /// Logika interakcji dla klasy EditWorkPlace.xaml
    /// </summary>
    public partial class EditWorkPlace : Window
    {
        private string _workPlaceName;
        public EditWorkPlace()
        {
            InitializeComponent();
        }

        public void SetWorkPlaceData(string workPlace)
        {
            _workPlaceName = workPlace;

            WorkPlace.Text = _workPlaceName;
        }

        private void EditPlace(object sender, RoutedEventArgs e)
        {
            if (WorkPlace.Text.Length > 0)
            {
                WorkPlacesManager workPlacesManager = new WorkPlacesManager();
                workPlacesManager.UpdatePlace(_workPlaceName, WorkPlace.Text);
                MessageBox.Show("Dokonano poprawnej edycji");
            } else
            {
                MessageBox.Show("Pole Miejsce dyżurowania nie może być puste jesli chcesz dokonać edycji!");
            }
            this.Close();
        }

        public void ChangeWorkerData(List<Worker> workerList)
        {
            if (_workPlaceName == WorkPlace.Text)
                return;

            WorkersManager workerManager = new WorkersManager();

            foreach (var item in workerList)
            {
                if (item.WorkPlaceName == _workPlaceName)
                {
                    item.WorkPlaceName = WorkPlace.Text;
                    workerManager.UpdateWorker(item);
                }
            }
        }
    }
}
