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

            WorkPlaces.SelectedValue = _worker.WorkPlaceName;

            AddDriverDay.Text = _worker.DriverHoursDay.ToString();
            AddDriverNight.Text = _worker.DriverHoursNight.ToString();
            AddExecutiveDay.Text = _worker.ExecutiveHoursDay.ToString();
            AddExecutiveNight.Text = _worker.ExecutiveHoursNight.ToString();

            ParseFreeDaysDataDisplay();
           
        }

        private void ParseFreeDaysDataDisplay()
        {
            if (_worker.FreeDaysDisplay == null)
                return;

            if (_worker.FreeDaysDisplay.Length > 1)
            {
                var parseFreeDaysDisplay = _worker.FreeDaysDisplay.Split(' ');
                _worker.FreeDaysDisplay = "";
                for (int i = 0; i < parseFreeDaysDisplay.Length; i++)
                {
                    if (i == parseFreeDaysDisplay.Length - 2)
                    {
                        _worker.FreeDaysDisplay += parseFreeDaysDisplay[i];
                        break;
                    }

                    _worker.FreeDaysDisplay += parseFreeDaysDisplay[i] + ",";
                }

                FreeDays.Text = _worker.FreeDaysDisplay;
            }

            if (_worker.FreeDaysDisplay.Length == 1)
                FreeDays.Text = _worker.FreeDaysDisplay;
        }

    }
}
