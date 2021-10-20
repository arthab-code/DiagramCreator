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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Grafik
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Worker _selectedWorker;
        public MainWindow()
        {
            InitializeComponent();

            WorkersManager workerManager = new WorkersManager();
            workerManager.LoadWorkersToList();
            WorkersListDisplay.ItemsSource = workerManager.Workers;
        }

        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
            AddWorker addWorker = new AddWorker();
            addWorker.ShowDialog();
            RefreshWindow();
        }

        private void RefreshWindow()
        {
            WorkersManager workerManager = new WorkersManager();
            workerManager.LoadWorkersToList();
            WorkersListDisplay.ItemsSource = workerManager.Workers;
        }
    }
}