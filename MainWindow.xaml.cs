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
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Calendar calendar;

        private WorkersManager workerManager;
        private Paths _paths = new Paths();
        private GenerateMonthlyDays _generateMonthlyDays;
        private CalculatedMonthlyDays _calculatedMonthlyDays;
        private CalculateDuty _calculateDuty;
        private MonthlyDays _monthlyDays;
        private DutyDisplayer _dutyDisplayer;
        public MainWindow()
        {
            InitializeComponent();

            if (CheckWorkPlaces())
            {
                ConfigWindow configWindow = new ConfigWindow();
                configWindow.ShowDialog();

                if (CheckWorkPlaces())
                    this.Close();
            }
            _generateMonthlyDays = new GenerateMonthlyDays();
            _calculatedMonthlyDays = new CalculatedMonthlyDays();
            _calculateDuty = new CalculateDuty();
            _monthlyDays = new MonthlyDays();
            _dutyDisplayer = new DutyDisplayer();

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            _generateMonthlyDays.Generate(_monthlyDays, year, month);

            RefreshWorkersList();
            RefreshWorkPlaces();

            CalculateDuty();

            _dutyDisplayer.RefreshDisplayer(_calculatedMonthlyDays, GeneralDays, DriverDay, DriverNight, ExecutiveDay, ExecutiveNight);

        }

        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
            AddWorker addWorker = new AddWorker();
            addWorker.ShowDialog();
            RefreshWorkersList();
            CalculateDuty();

            _dutyDisplayer.RefreshDisplayer(_calculatedMonthlyDays, GeneralDays, DriverDay, DriverNight, ExecutiveDay, ExecutiveNight);
        }

        private void RefreshWorkersList()
        {
            workerManager = new WorkersManager();
            workerManager.LoadWorkersToList();
            WorkersListDisplay.ItemsSource = workerManager.Workers;
        }

        private void DeleteWorker(object sender, RoutedEventArgs e)
        {
            if (WorkersListDisplay.SelectedItem == null)
                return;

            var workerTemp = (Worker)WorkersListDisplay.SelectedItem;
            workerManager.DeleteWorker(workerTemp.Name, workerTemp.Surname);

            MessageBox.Show("Usunięto pracownika : " + workerTemp.Name + " " + workerTemp.Surname);
            RefreshWorkersList();
            CalculateDuty();

            _dutyDisplayer.RefreshDisplayer(_calculatedMonthlyDays, GeneralDays, DriverDay, DriverNight, ExecutiveDay, ExecutiveNight);
        }

        private void EditWorker(object sender, RoutedEventArgs e)
        {
            if (WorkersListDisplay.SelectedItem == null)
                return;

            EditWorker editWorker = new EditWorker();
            editWorker.SetWorkerData((Worker)WorkersListDisplay.SelectedItem);
            editWorker.ShowDialog();
            RefreshWorkersList();

            CalculateDuty();

            _dutyDisplayer.RefreshDisplayer(_calculatedMonthlyDays, GeneralDays, DriverDay, DriverNight, ExecutiveDay, ExecutiveNight);
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
            if (WorkPlaces.SelectedItem == null)
                return;
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
            if (WorkPlaces.SelectedItem == null)
                return;
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

        private bool CheckWorkPlaces()
        {
            var directories = Directory.GetDirectories(_paths._workerPlacePath);

            if (directories.Length == 0)
            {
                return true;
            }

            return false;
        }

        private void SelectedDateChanges(object sender, SelectionChangedEventArgs e)
        {
            calendar = (Calendar)sender;

            int year = calendar.SelectedDate.Value.Year;
            int month = calendar.SelectedDate.Value.Month;

            _generateMonthlyDays.Generate(_monthlyDays, year, month);

            CalculateDuty();

            _dutyDisplayer.RefreshDisplayer(_calculatedMonthlyDays, GeneralDays, DriverDay, DriverNight, ExecutiveDay, ExecutiveNight);

            MessageBox.Show("Zmianieniono datę grafiku na : "+year.ToString()+" "+month.ToString());
        }

        private void CalculateDuty()
        {
            _calculateDuty.CalculateDriverDay(_monthlyDays, _calculatedMonthlyDays, workerManager.Workers, WorkPlaces.SelectedItem.ToString());
            _calculateDuty.CalculateDriverNight(_monthlyDays, _calculatedMonthlyDays, workerManager.Workers, WorkPlaces.SelectedItem.ToString());
            _calculateDuty.CalculateExecutiveDay(_monthlyDays, _calculatedMonthlyDays, workerManager.Workers, WorkPlaces.SelectedItem.ToString());
            _calculateDuty.CalculateExecutiveNight(_monthlyDays, _calculatedMonthlyDays, workerManager.Workers, WorkPlaces.SelectedItem.ToString());
            _calculateDuty.CalculateMonthlyDays(_monthlyDays, _calculatedMonthlyDays);
        }

        private void WorkPlacesSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (WorkPlaces.SelectedItem == null)
                return;

            CalculateDuty();

            _dutyDisplayer.RefreshDisplayer(_calculatedMonthlyDays, GeneralDays, DriverDay, DriverNight, ExecutiveDay, ExecutiveNight);
        }

        private void GenerateDiagram_Click(object sender, RoutedEventArgs e)
        {
            int month = calendar.SelectedDate.Value.Month;
            int year = calendar.SelectedDate.Value.Year;
            int days = DateTime.DaysInMonth(year, month);
            WorkDiagram workDiagram = new WorkDiagram(days);
            DiagramSetter diagramSetter = new DiagramSetter(workerManager.Workers, workDiagram);
            PermamentDiagramCreator permDiagram = new PermamentDiagramCreator(workDiagram);
            permDiagram.AddDriverDays();
            permDiagram.AddDriverNight();
            DiagramShowHelper dsh = new DiagramShowHelper();
            dsh.SetDiagramCreator(permDiagram);
            dsh.ShowDialog();
        }
    }
}
