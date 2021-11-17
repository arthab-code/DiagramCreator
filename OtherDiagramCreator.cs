using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public class OtherDiagramCreator : DiagramCreator
    {
        public OtherDiagramCreator(WorkDiagram workDiagram)
        {
            _workDiagram = workDiagram;
            _gapDuty = 0;
            _stepDuty = 0;
            _loopSafety = 1000;
        }

        public override void Create(string workPlace)
        {
            AddDriverDays(workPlace);
            AddExecutiveDays(workPlace);
            AddDriverNight(workPlace);
            AddExecutiveNight(workPlace);
        }

        private void CreateWorkerDiagramArray(int monthDays)
        {
            foreach (var item in _workDiagram.OtherAgreementWorkers)
            {
                item.WorkDiagramDay = new char[monthDays];
                item.WorkDiagramNight = new char[monthDays];
            }
        }

        private void AddDriverDays(string workPlace)
        {
            _driverDay = new List<Worker>();

            foreach (var item in _workDiagram.OtherAgreementWorkers)
            {
                if (item.DriverHoursDay > 0 && item.WorkPlaceName == workPlace)
                    _driverDay.Add(item);

            }

        }

        private void AddExecutiveDays(string workPlace)
        {
            _executiveDay = new List<Worker>();

            foreach (var item in _workDiagram.OtherAgreementWorkers)
            {
                if (item.DriverHoursNight > 0 && item.WorkPlaceName == workPlace)
                    _driverNight.Add(item);

            }

        }

        private void AddDriverNight(string workPlace)
        {
            _driverNight = new List<Worker>();

            foreach (var item in _workDiagram.OtherAgreementWorkers)
            {
                if (item.ExecutiveHoursDay > 0 && item.WorkPlaceName == workPlace)
                    _executiveDay.Add(item);

            }

        }

        private void AddExecutiveNight(string workPlace)
        {
            _executiveNight = new List<Worker>();

            foreach (var item in _workDiagram.OtherAgreementWorkers)
            {
                if (item.ExecutiveHoursNight > 0 && item.WorkPlaceName == workPlace)
                    _executiveNight.Add(item);

            }
        }
    }
}
