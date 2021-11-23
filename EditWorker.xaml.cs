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
    /// Logika interakcji dla klasy EditWorker.xaml
    /// </summary>
    public partial class EditWorker : Window
    {
        private Worker _worker;

        public EditWorker()
        {
            InitializeComponent();
        }

        private void SaveEdit(object sender, RoutedEventArgs e)
        {
            WorkerEditing();
        }
        public void SetWorkerData(Worker worker)
        {
            _worker = worker;
            NameDisplay.Text = _worker.Name;
            SurnameDisplay.Text = _worker.Surname;

            switch(_worker.AgreementType)
            {
                case AgreementType.Permanent:
                    Permanent.IsChecked = true;
                    break;

                case AgreementType.Other:
                    Other.IsChecked = true;
                    break;
            }

            switch (_worker.WorkSystem)
            {
                case WorkSystem.FullDuty:
                    FullDuty.IsChecked = true;
                    break;

                case WorkSystem.HalfDuty:
                    HalfDuty.IsChecked = true;
                    break;
            }

            switch (_worker.WorkType)
            {
                case WorkType.Hybrid:
                    Hybrid.IsChecked = true;
                    break;

                case WorkType.Executive:
                    Paramedic.IsChecked = true;
                    break;

                case WorkType.Driver:
                    Driver.IsChecked = true;
                    break;

                case WorkType.Other:
                    /**********************/
                    break;
            }

            WorkPlacesManager workPlacesManager = new WorkPlacesManager();
            workPlacesManager.LoadWorkPlacesToList();
            WorkPlaces.ItemsSource = workPlacesManager.WorkPlaces;

            WorkPlaces.SelectedItem = _worker.WorkPlaceName;

            AddDriverDay.Text = _worker.DriverDutyDay.ToString();
            AddDriverNight.Text = _worker.DriverDutyNight.ToString();
            AddExecutiveDay.Text = _worker.ExecutiveDutyDay.ToString();
            AddExecutiveNight.Text = _worker.ExecutiveDutyNight.ToString();
            FreeDays.Text = _worker.FreeDaysDisplay;
        }

        private void WorkerEditing()
        {
            int driverDay, driverNight, executiveDay, executiveNight;
            try
            {
                driverDay = int.Parse(AddDriverDay.Text);
                driverNight = int.Parse(AddDriverNight.Text);
                executiveDay = int.Parse(AddExecutiveDay.Text);
                executiveNight = int.Parse(AddExecutiveNight.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + "Dane w polach określających liczbę dyżurów powinny mieć wartość liczbową!");
                this.Close();
                return;
            }

            if (FreeDaysParser.ParseFreeDays(_worker.FreeDays, FreeDays.Text))
            {
                InitializeRadioButtons();
                _worker.DriverDutyDay = driverDay;
                _worker.DriverDutyNight = driverNight;
                _worker.ExecutiveDutyDay = executiveDay;
                _worker.ExecutiveDutyNight = executiveNight;
                _worker.WorkPlaceName = WorkPlaces.SelectedValue.ToString();

                WorkersManager workersManager = new WorkersManager();
                workersManager.UpdateWorker(_worker);

                MessageBox.Show("Poprawnie przeprowadzono edycję pracownika : " + _worker.Name + " " + _worker.Surname);
                this.Close();
            }
        }

        private void InitializeRadioButtons()
        {
            if (Permanent.IsChecked == true)
            {
                _worker.AgreementType = AgreementType.Permanent;
                _worker.WorkSystem = WorkSystem.HalfDuty;
            }

            if (Other.IsChecked == true)
                _worker.AgreementType = AgreementType.Other;

            if (FullDuty.IsChecked == true)
                _worker.WorkSystem = WorkSystem.FullDuty;
            if (HalfDuty.IsChecked == true)
                _worker.WorkSystem = WorkSystem.HalfDuty;

            if (Paramedic.IsChecked == true)
                _worker.WorkType = WorkType.Executive;
            if (Hybrid.IsChecked == true)
                _worker.WorkType = WorkType.Hybrid;
            if (Driver.IsChecked == true)
                _worker.WorkType = WorkType.Driver;
            /*
            if (OtherType.IsChecked == true)
                workType = WorkType.Other;
            */
        }

    }
}
