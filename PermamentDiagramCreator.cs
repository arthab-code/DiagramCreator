using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public class PermamentDiagramCreator
    {
        private WorkDiagram _workDiagram;
        public PermamentDiagramCreator(WorkDiagram workDiagram)
        {
            _workDiagram = workDiagram;

            CreateWorkerDiagramArray(_workDiagram.MonthDays);
        }

        private void CreateWorkerDiagramArray(int monthDays)
        {
            foreach (var item in _workDiagram.PermanentWorkers)
                item.WorkDiagram = new char[2, monthDays];

            foreach (var item in _workDiagram.OtherAgreementWorkers)
                item.WorkDiagram = new char[2, monthDays];
        }

        private void AddDriverDays()
        {
            List<Worker> driverDayPermanent = new List<Worker>();

            foreach (var item in _workDiagram.PermanentWorkers)
            {
                if (item.DriverHoursDay > 0)
                    driverDayPermanent.Add(item);
            }

        }

        private void AddDriverNight()
        {
            List<Worker> driverNightPermanent = new List<Worker>();

            foreach (var item in _workDiagram.PermanentWorkers)
            {
                if (item.DriverHoursNight > 0)
                    driverNightPermanent.Add(item);
            }
        }

        private void AddExecutiveDays()
        {
            List<Worker> ExecutiveDayPermanent = new List<Worker>();

            foreach (var item in _workDiagram.PermanentWorkers)
            {
                if (item.ExecutiveHoursDay > 0)
                    ExecutiveDayPermanent.Add(item);
            }
        }

        private void AddExecutiveNight()
        {
            List<Worker> executiveNightPermanent = new List<Worker>();

            foreach (var item in _workDiagram.PermanentWorkers)
            {
                if (item.ExecutiveHoursNight> 0)
                    executiveNightPermanent.Add(item);
            }
        }

    }
}
