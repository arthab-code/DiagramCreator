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
    /// Logika interakcji dla klasy AddWorker.xaml
    /// </summary>
    public partial class AddWorker : Window
    {
        private bool _checkedParseFreeDays = true;
        private WorkSystem _workSystem;
        private AgreementType _agreementType;
        private WorkType _workType;

        public AddWorker()
        {
            InitializeComponent();

            LoadWorkPlaces();
        }

        private void ADD_Click(object sender, RoutedEventArgs e)
        {
            InitializeRadioButtons();
            
            if (!CheckDataFilled())
            {
                this.Close();
                return;
            }


                if (_agreementType == AgreementType.Permanent && _workType == WorkType.Hybrid)
                {
                   AddingWorker
                   (AddName.Text, AddSurname.Text, _agreementType, _workSystem, _workType,
                    WorkPlaces.SelectedItem.ToString(), 3, 4, 4, 3);
                }

                if (_agreementType == AgreementType.Permanent && _workType == WorkType.Executive)
                {

                   AddingWorker
                   (AddName.Text, AddSurname.Text, _agreementType, _workSystem, _workType,
                   WorkPlaces.SelectedItem.ToString(), 0, 0, 7, 7);

                }

                if (_agreementType == AgreementType.Permanent && _workType == WorkType.Driver)
                {

                    AddingWorker
                    (AddName.Text, AddSurname.Text, _agreementType, _workSystem, _workType,
                    WorkPlaces.SelectedItem.ToString(), 7, 7, 0, 0);

                }


                if (_agreementType == AgreementType.Other)
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

                AddingWorker
                (AddName.Text, AddSurname.Text, _agreementType, _workSystem, _workType,
                WorkPlaces.SelectedItem.ToString(), driverDay, driverNight, executiveDay, executiveNight);

                }


            this.Close();
        }

      /*  private void ParseFreeDays(Worker worker)
        {
            var freeDaysString = FreeDays.Text;

            if (freeDaysString.Length == 0)
                return;

            if (freeDaysString.Length == 1)
            {
                worker.FreeDays.Add(byte.Parse(freeDaysString));
                return;
            }

            string[] freeDaysStringArray;

            try
            {
                freeDaysStringArray = freeDaysString.Split(',');
                for (int i = 0; i < freeDaysStringArray.Length; i++)
                {
                    worker.FreeDays.Add(byte.Parse(freeDaysStringArray[i]));
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + "Liczby w polu DNI WOLNE powinny być oddzielone od siebie znakiem ','  !!");
                _checkedParseFreeDays = false;
                return;
            }
        } */

        private void InitializeRadioButtons()
        {
            _workSystem = WorkSystem.NotSelected;
            _agreementType = AgreementType.Permanent;
            _workType = WorkType.Hybrid;

            if (Permanent.IsChecked == true)
            {
                _agreementType = AgreementType.Permanent;
                _workSystem = WorkSystem.HalfDuty;
            }

            if (Other.IsChecked == true)
                _agreementType = AgreementType.Other;

            if (FullDuty.IsChecked == true)
                _workSystem = WorkSystem.FullDuty;
            if (HalfDuty.IsChecked == true)
                _workSystem = WorkSystem.HalfDuty;

            if (Paramedic.IsChecked == true)
                _workType = WorkType.Executive;
            if (Hybrid.IsChecked == true)
                _workType = WorkType.Hybrid;
            if (Driver.IsChecked == true)
                _workType = WorkType.Driver;
            /*
            if (OtherType.IsChecked == true)
                workType = WorkType.Other;
            */
        }

        private bool CheckDataFilled()
        {

            if (WorkPlaces.SelectedItem == null || AddName.Text == "" || AddSurname.Text == "" || _workSystem == WorkSystem.NotSelected)
            {
                MessageBox.Show("WYPEŁNIJ WSZYSTKIE DANE !");
                return false;
            } else
            {
                return true;
            }
        }

        private void AddingWorker(string name, string surname, AgreementType agreementType, WorkSystem workSystem, WorkType workType, string workPlace, int driverDay, int driverNight, int executiveDay, int executiveNight)
        {
            Worker workerBuild =  new WorkerBuilder()
                   .SetName(name)
                   .SetSurname(surname)
                   .SetAgreementType(agreementType)
                   .SetWorkSystem(workSystem)
                   .SetWorkType(workType)
                   .SetWorkPlaceName(workPlace)
                   .SetDriverHoursDay(driverDay)
                   .SetDriverHoursNight(driverNight)
                   .SetExecutiveHoursDay(executiveDay)
                   .SetExecutiveHoursNight(executiveNight)
                   .Build();

            if (FreeDaysParser.ParseFreeDays(workerBuild.FreeDays, FreeDays.Text))
            {
                WorkersManager workersManager = new WorkersManager();
                workersManager.Create(workerBuild);
                MessageBox.Show("Dodano pracownika " + workerBuild.Name + " " + workerBuild.Surname + " WORK TYPE : " + workerBuild.WorkType);
            }
        }


        private void LoadWorkPlaces()
        {
            WorkPlacesManager workPlacesManager = new WorkPlacesManager();

            workPlacesManager.LoadWorkPlacesToList();

            WorkPlaces.ItemsSource = workPlacesManager.WorkPlaces;
        }
    }
}
