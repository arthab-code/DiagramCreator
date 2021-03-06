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
        public DiagramShowHelper(int year, int month, string workPlace)
        {
            InitializeComponent();
            DataContext = this;
            DataDisplayer.Text = "Miesiąc : " + month.ToString() + " Rok : " + year.ToString() + "/ ZESPÓŁ : "+workPlace;
        }

        public void SetDiagramCreator(List<Worker> workers, WorkDiagram workDiagram, string workPlace)
        {
            _workers = workers;
            _workDiagram = workDiagram;
            ShowDiagram(workPlace);
            SetWarnings();
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
                pD.PrintVisual(PrintDisplayer, "Diagram");
            }
        }

        private void SetWarnings()
        {
            string temp = "";
            int i = 0;
            foreach (var item in _workDiagram.WorkDiagramDayDriver)
            {
                i++;
                if (item != 'x')
                    temp += i.ToString() + " , ";
            }
            driverDayWarning.Text = temp;

            temp = "";
            i = 0;
            foreach (var item in _workDiagram.WorkDiagramNightDriver)
            {
                i++;
                if (item != 'x')
                    temp += i.ToString() + " , ";
            }
            driverNightWarning.Text = temp;

            temp = "";
            i = 0;
            foreach (var item in _workDiagram.WorkDiagramDayExecutive)
            {
                i++;
                if (item != 'x')
                    temp += i.ToString() + " , ";
            }
            executiveDayWarning.Text = temp;

            temp = "";
            i = 0;
            foreach (var item in _workDiagram.WorkDiagramNightExecutive)
            {
                i++;
                if (item != 'x')
                    temp += i.ToString() + " , ";
            }
            executiveNightWarning.Text = temp;
        }
    }
}
