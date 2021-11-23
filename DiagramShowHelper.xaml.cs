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
    /// Logika interakcji dla klasy DiagramShowHelper.xaml
    /// </summary>
    public partial class DiagramShowHelper : Window
    {
        private List<Worker> _workers;
        private WorkDiagram _workDiagram;
        public DiagramShowHelper()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void SetDiagramCreator(List<Worker> workers, WorkDiagram workDiagram, string workPlace)
        {
            _workers = workers;
            _workDiagram = workDiagram;
            ShowDiagram(workPlace);
        }

        public void ShowDiagram(string workPlace)
        {
            foreach(var item in _workers)
            {
                item.DiagramDisplayer = new string[_workDiagram.MonthDays];

                for (int i = 0; i < _workDiagram.MonthDays; i++)
                {
                    item.DiagramDisplayer[i] += item.WorkDiagramDay[i].ToString() + " " + item.WorkDiagramNight[i].ToString();
                }
            }
            List<Worker> tempWorkers = new List<Worker>();

            foreach(var item in _workers)
            {
                if (item.WorkPlaceName == workPlace)
                    tempWorkers.Add(item);
            }

            DiagramDisplayer.ItemsSource = tempWorkers;

            for (int i = 0; i < _workDiagram.MonthDays; i++)
            {
                GridViewColumn gvc = new GridViewColumn();
                gvc.DisplayMemberBinding = new Binding("DiagramDisplayer["+i+"]");
                gvc.Width = 40;
                gvc.Header = (i + 1).ToString();
                Columns.Columns.Add(gvc);         
            }          
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pD = new PrintDialog();

            if (pD.ShowDialog() == true)
            {
                pD.PrintVisual(DiagramDisplayer, "Diagram");
            }
        }
    }
}
