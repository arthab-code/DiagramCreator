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
        public Worker _selectedWorker;
        public MainWindow()
        {
            InitializeComponent();

            Worker worker = new WorkerBuilder()
                 .SetName("Artur")
                 .SetSurname("Habrajski")
                 .SetWorkDaysPerMonth(18)
                 .SetDriverHoursDay(5)
                 .SetWorkPlaceName("s0450")
                 .SetWorkType(WorkType.Hybrid)
                 .SetAgreementType(AgreementType.Permanent)
                 .Build();
            Worker worker2 = new WorkerBuilder()
     .SetName("Artur43434")
     .SetSurname("Habrajski34343")
     .SetWorkDaysPerMonth(18)
     .SetDriverHoursDay(5)
     .SetWorkPlaceName("s0450")
     .SetWorkType(WorkType.Hybrid)
     .SetAgreementType(AgreementType.Permanent)
     .Build();
            Worker worker3 = new WorkerBuilder()
     .SetName("Arddftur")
     .SetSurname("Habrajsfdfdsski")
     .SetWorkDaysPerMonth(18)
     .SetDriverHoursDay(5)
     .SetWorkPlaceName("s0450")
     .SetWorkType(WorkType.Hybrid)
     .SetAgreementType(AgreementType.Permanent)
     .Build();

            var saveSystem = new WorkersFileDatabase();
            saveSystem.CreateWorker(worker);
            saveSystem.CreateWorker(worker2);
            saveSystem.CreateWorker(worker3);
            WorkersManager _workersManager = new WorkersManager();
            _workersManager.LoadWorkersToList();

            //_workersManager.DeleteWorker("Artur", "Habrajski");

            //    worker.WorkDaysPerMonth = 30;
            //    _workersManager.UpdateWorker(worker);
            foreach (var w in _workersManager.Workers)
            {
                // Console.WriteLine(w.Name);
               // WorkersList.ItemsSource = _workersManager.Workers;
                
            }

        }

        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
            AddWorker addWorker = new AddWorker();
            addWorker.ShowDialog();
        }
    }
}
