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
        }

        public void SetDiagramCreator(PermamentDiagramCreator permDiagram)
        {
            _permDiagram = permDiagram;
            ShowDiagram();
        }

        public void ShowDiagram()
        {
            textBlock.Text = "\t";
            foreach (Worker item in _permDiagram.WorkDiagram.PermanentWorkers)
            {
                textBlock.Text += item.Name + " " + item.Surname + "   ";
                for (int i=0; i< _permDiagram.WorkDiagram.MonthDays; i++)
                {
                    textBlock.Text += i+1+" : "+item.WorkDiagramDay[i] + "  " + item.WorkDiagramNight[i] + " | ";
                }

                textBlock.Text += "\n\t";
            }

            FULLWORKDAY.Text = "\t";

            
                for (int i = 0; i < _permDiagram.WorkDiagram.MonthDays; i++)
                {
                    FULLWORKDAY.Text += i + 1 + " : " + _permDiagram.WorkDiagram.WorkDiagramDayDriver[i] + " | ";
                }

            FULLWORKDAY.Text += "\n\t";

            FULLWORKNIGHT.Text = "\t";


            for (int i = 0; i < _permDiagram.WorkDiagram.MonthDays; i++)
            {
                FULLWORKNIGHT.Text += i + 1 + " : " + _permDiagram.WorkDiagram.WorkDiagramNightDriver[i] + " | ";
            }

            FULLWORKNIGHT.Text += "\n\t"; 


        }
    }
}
