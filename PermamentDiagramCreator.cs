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
        private List<Worker> _driverDayPermanent;
        private List<Worker> _driverNightPermanent;
        private int _gapDuty;

        public List<Worker> DriverDayPermanent { get => _driverDayPermanent; set => _driverDayPermanent = value; }
        public WorkDiagram WorkDiagram { get => _workDiagram; set => _workDiagram = value; }
        public List<Worker> DriverNightPermanent { get => _driverNightPermanent; set => _driverNightPermanent = value; }

        public PermamentDiagramCreator(WorkDiagram workDiagram)
        {
            _workDiagram = workDiagram;
            _gapDuty = 4;

            CreateWorkerDiagramArray(_workDiagram.MonthDays);
        }

        private void CreateWorkerDiagramArray(int monthDays)
        {
            foreach (var item in _workDiagram.PermanentWorkers)
            {
                item.WorkDiagramDay = new char[monthDays];
                item.WorkDiagramNight = new char[monthDays];
            }
        }

        private void CalculateMonthlyDuty(Worker worker)
        {
            worker.WorkDaysPerMonth = worker.DriverHoursDay + worker.DriverHoursNight + worker.ExecutiveHoursDay + worker.ExecutiveHoursNight;
        }

        public void AddDriverDays()
        {
           _driverDayPermanent = new List<Worker>();

            foreach (var item in _workDiagram.PermanentWorkers)
            {
                if (item.DriverHoursDay > 0)
                {
                    CalculateMonthlyDuty(item);
                    _driverDayPermanent.Add(item);
                }
            }
            Random rand = new Random();

            for (int i = 0; i < _driverDayPermanent.Count; i++)
            {
                int stepDuty = 0;

                while (_driverDayPermanent[i].DriverHoursDay > 0)
                {
                    if (stepDuty > (_workDiagram.MonthDays-1))
                    {
                        stepDuty -= (_workDiagram.MonthDays-1);
                    }

                    if (i == 0)
                    {
                        _workDiagram.WorkDiagramDayDriver[stepDuty] = 'x';
                        _driverDayPermanent[i].WorkDiagramDay[stepDuty] = 'K';
                        _driverDayPermanent[i].DriverHoursDay -= 1;
                        stepDuty += _gapDuty;
                        continue;

                    } 
                    
                    if (_workDiagram.WorkDiagramDayDriver[stepDuty] == 'x')
                    {
                         stepDuty++;

                        continue;
                    } 
                    
                    _workDiagram.WorkDiagramDayDriver[stepDuty] = 'x';
                    _driverDayPermanent[i].WorkDiagramDay[stepDuty] = 'K';
                    _driverDayPermanent[i].DriverHoursDay -= 1;
                    stepDuty += _gapDuty;
                  
                }
            }

        }

        public void AddDriverNight()
        {
            _driverNightPermanent = new List<Worker>();

            foreach (var item in _workDiagram.PermanentWorkers)
            {
                if (item.DriverHoursNight > 0)
                    _driverNightPermanent.Add(item);
            }

            Random rand = new Random();

           
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
