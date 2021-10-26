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

namespace Grafik
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WorkersManager workerManager; 
        
        public MainWindow()
        {
            InitializeComponent();

            RefreshWorkersList();
            RefreshWorkPlaces();
        }

        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
            AddWorker addWorker = new AddWorker();
            addWorker.ShowDialog();
            RefreshWorkersList();
        }

        private void RefreshWorkersList()
        {
            workerManager = new WorkersManager();
            workerManager.LoadWorkersToList();
            WorkersListDisplay.ItemsSource = workerManager.Workers;
        }

        private void DeleteWorker(object sender, RoutedEventArgs e)
        {
            var workerTemp = (Worker)WorkersListDisplay.SelectedItem;
            workerManager.DeleteWorker(workerTemp.Name, workerTemp.Surname);

            MessageBox.Show("Usunięto pracownika : " + workerTemp.Name + " " + workerTemp.Surname);
            RefreshWorkersList();
        }

        private void EditWorker(object sender, RoutedEventArgs e)
        {
            EditWorker editWorker = new EditWorker();
            editWorker.SetWorkerData((Worker)WorkersListDisplay.SelectedItem);
            editWorker.ShowDialog();
            RefreshWorkersList();
        }

        private void RefreshWorkPlaces()
        {
            WorkPlacesManager workPlacesManager = new WorkPlacesManager();

            workPlacesManager.LoadWorkPlacesToList();

            WorkPlaces.ItemsSource = workPlacesManager.WorkPlaces;
        }

        private void AddWorkPlace(object sender, RoutedEventArgs e)
        {
            AddWorkPlace addWorkPlace = new AddWorkPlace();
            addWorkPlace.ShowDialog();
            RefreshWorkPlaces();
        }

        private void EditWorkPlace(object sender, RoutedEventArgs e)
        {
            EditWorkPlace editWorkPlace = new EditWorkPlace();
            editWorkPlace.SetWorkPlaceData((string)WorkPlaces.SelectedItem);
            editWorkPlace.ShowDialog();
            editWorkPlace.ChangeWorkerData(workerManager.Workers);
            RefreshWorkersList();
            RefreshWorkPlaces();

            /* TUTAJ WYMAGANA FUNKCJA EDYCJI WSZYSTKICH PRACOWNIKOW NA NOWE MIEJSCE PRACY !! */
        }

        private void DeletePlace(object sender, RoutedEventArgs e)
        {
            WorkPlacesManager workPlacesManager = new WorkPlacesManager();
            var workPlaceTemp = (string)WorkPlaces.SelectedItem;
            workPlacesManager.DeleteWorkPlace(workPlaceTemp);
            MessageBox.Show("Usunięto miejsce pracy : " + workPlaceTemp);
            ChangeWorkerDataWhenDeleteWorkPlace(workerManager.Workers, workPlaceTemp);
            RefreshWorkersList();
            RefreshWorkPlaces();
        }

        public void ChangeWorkerDataWhenDeleteWorkPlace(List<Worker> workerList, string workPlaceNameOld)
        {
            WorkersManager workerManager = new WorkersManager();

            foreach (var item in workerList)
            {
                if (item.WorkPlaceName == workPlaceNameOld)
                {
                    item.WorkPlaceName = "Usunięto UWAGA!";
                    workerManager.UpdateWorker(item);
                }
            }
        }
    }
}
