﻿using System;
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
        public AddWorker()
        {
            InitializeComponent();

            LoadWorkPlaces();
        }

        private void ADD_Click(object sender, RoutedEventArgs e)
        {
            WorkSystem workSystem = WorkSystem.FullDuty;
            AgreementType agreementType = AgreementType.Permanent;
            WorkType workType = WorkType.Hybrid;

            if (Permanent.IsChecked == true)
                agreementType = AgreementType.Permanent;
            if (Other.IsChecked == true)
                agreementType = AgreementType.Other;

            if (FullDuty.IsChecked == true)
                workSystem = WorkSystem.FullDuty;
            if (HalfDuty.IsChecked == true)
                workSystem = WorkSystem.HalfDuty;

            if (Paramedic.IsChecked == true)
                workType = WorkType.Executive;
            if (Hybrid.IsChecked == true)
                workType = WorkType.Hybrid;
            if (Driver.IsChecked == true)
                workType = WorkType.Driver;
            if (Other.IsChecked == true)
                workType = WorkType.Other;

            Worker workerBuild = new Worker();

            if (agreementType == AgreementType.Permanent && workType == WorkType.Hybrid)
            {
                workerBuild = new WorkerBuilder()
                    .SetName(AddName.Text)
                    .SetSurname(AddSurname.Text)
                    .SetAgreementType(agreementType)
                    .SetWorkSystem(workSystem)
                    .SetWorkType(workType)
                    .SetWorkPlaceName(WorkPlaces.SelectedItem.ToString())
                    .SetDriverHoursDay(3)
                    .SetDriverHoursNight(4)
                    .SetExecutiveHoursDay(4)
                    .SetExecutiveHoursNight(3)
                    .Build();
            }

            if (agreementType == AgreementType.Permanent && workType == WorkType.Executive)
            {
                workerBuild = new WorkerBuilder()
                    .SetName(AddName.Text)
                    .SetSurname(AddSurname.Text)
                    .SetAgreementType(agreementType)
                    .SetWorkSystem(workSystem)
                    .SetWorkType(workType)
                    .SetWorkPlaceName(WorkPlaces.SelectedItem.ToString())
                    .SetDriverHoursDay(0)
                    .SetDriverHoursNight(0)
                    .SetExecutiveHoursDay(7)
                    .SetExecutiveHoursNight(7)
                    .Build();
            }

            if (agreementType == AgreementType.Permanent && workType == WorkType.Driver)
            {
                workerBuild = new WorkerBuilder()
                    .SetName(AddName.Text)
                    .SetSurname(AddSurname.Text)
                    .SetAgreementType(agreementType)
                    .SetWorkSystem(workSystem)
                    .SetWorkType(workType)
                    .SetWorkPlaceName(WorkPlaces.SelectedItem.ToString())
                    .SetWorkPlaceName(WorkPlaces.SelectedItem.ToString())
                    .SetDriverHoursDay(7)
                    .SetDriverHoursNight(7)
                    .SetExecutiveHoursDay(0)
                    .SetExecutiveHoursNight(0)
                    .Build();
            }


            if (agreementType == AgreementType.Other)
            {
                workerBuild = new WorkerBuilder()
                    .SetName(AddName.Text)
                    .SetSurname(AddSurname.Text)
                    .SetAgreementType(agreementType)
                    .SetWorkSystem(workSystem)
                    .SetWorkType(workType)
                    .SetWorkPlaceName(WorkPlaces.SelectedItem.ToString())
                    .SetDriverHoursDay(int.Parse(AddDriverDay.Text))
                    .SetDriverHoursNight(int.Parse(AddDriverNight.Text))
                    .SetExecutiveHoursDay(int.Parse(AddExecutiveDay.Text))
                    .SetExecutiveHoursNight(int.Parse(AddExecutiveNight.Text))
                    .Build();
            }

            ParseFreeDays(workerBuild);

            WorkersManager workersManager = new WorkersManager();
            workersManager.Create(workerBuild);
            MessageBox.Show("Dodano pracownika " + workerBuild.Name + " " + workerBuild.Surname);
           
            this.Close();



        }

        private void ParseFreeDays(Worker worker)
        {
            var freeDaysString = FreeDays.Text;
            var freeDaysStringArray = freeDaysString.Split(',');

            for (int i=0; i < freeDaysStringArray.Length; i++)
            {
                worker.FreeDays.Add(byte.Parse(freeDaysStringArray[i]));
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