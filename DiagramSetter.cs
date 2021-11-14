using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public class DiagramSetter
    {
        private WorkDiagram _workDiagram;
        public DiagramSetter(List<Worker> workersList, WorkDiagram workDiagram)
        {
            _workDiagram = workDiagram;

            foreach(var item in workersList)
            {
                if (item.AgreementType == AgreementType.Permanent)
                    _workDiagram.PermanentWorkers.Add(item);
                else if (item.AgreementType == AgreementType.Other)
                    _workDiagram.OtherAgreementWorkers.Add(item);
            }

            _workDiagram.WorkDiagramDayDriver = new char[_workDiagram.MonthDays];
            _workDiagram.WorkDiagramNightDriver = new char[_workDiagram.MonthDays];
            _workDiagram.WorkDiagramDayExecutive = new char[_workDiagram.MonthDays];
            _workDiagram.WorkDiagramNightExecutive = new char[_workDiagram.MonthDays];
        }
    }
}
