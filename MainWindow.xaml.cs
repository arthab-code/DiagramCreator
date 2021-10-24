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
        }
    }
}
