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
        private PermamentDiagramCreator _permDiagram;
        public DiagramShowHelper()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void SetDiagramCreator(PermamentDiagramCreator permDiagram)
        {
            _permDiagram = permDiagram;
            ShowDiagram();
        }

        public void ShowDiagram()
        {
            foreach(var item in _permDiagram.WorkDiagram.PermanentWorkers)
            {
                item.DiagramDisplayer = new string[_permDiagram.WorkDiagram.MonthDays];

                for (int i = 0; i < _permDiagram.WorkDiagram.MonthDays; i++)
                {
                    item.DiagramDisplayer[i] += item.WorkDiagramDay[i].ToString() + " " + item.WorkDiagramNight[i].ToString();
                }
            }

            DiagramDisplayer.ItemsSource = _permDiagram.WorkDiagram.PermanentWorkers;

            for (int i = 0; i < _permDiagram.WorkDiagram.MonthDays; i++)
            {
                GridViewColumn gvc = new GridViewColumn();
                gvc.DisplayMemberBinding = new Binding("DiagramDisplayer["+i+"]");
                gvc.Width = 40;
                gvc.Header = (i + 1).ToString();
                Columns.Columns.Add(gvc);           
            }

           
        }
    }
}
