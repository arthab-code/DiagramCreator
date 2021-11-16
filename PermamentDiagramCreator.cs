using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Grafik
{
    public class PermamentDiagramCreator
    {
        private WorkDiagram _workDiagram;
        private List<Worker> _driverDayPermanent;
        private List<Worker> _driverNightPermanent;
        private List<Worker> _executiveDayPermanent;
        private List<Worker> _executiveNightPermanent;
        private int _gapDuty;
        private int loopSafety = 1000;

        public List<Worker> DriverDayPermanent { get => _driverDayPermanent; set => _driverDayPermanent = value; }
        public WorkDiagram WorkDiagram { get => _workDiagram; set => _workDiagram = value; }
        public List<Worker> DriverNightPermanent { get => _driverNightPermanent; set => _driverNightPermanent = value; }
        public List<Worker> ExecutiveDayPermanent { get => _executiveDayPermanent; set => _executiveDayPermanent = value; }
        public List<Worker> ExecutiveNightPermanent { get => _executiveNightPermanent; set => _executiveNightPermanent = value; }

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
                int stepDuty = rand.Next(0, _workDiagram.MonthDays);
                int safetyCounter = 0;

                while (_driverDayPermanent[i].DriverHoursDay > 0)
                {
                    if (safetyCounter > loopSafety)
                    {
                        MessageBox.Show("SAFETY  EXIT");
                        break;
                    }

                    if (stepDuty > (_workDiagram.MonthDays-1))
                    {
                        //stepDuty -= (_workDiagram.MonthDays-1);
                        stepDuty = 0;
                    }
                    
                    if (_workDiagram.WorkDiagramDayDriver[stepDuty] == 'x' || _driverDayPermanent[i].FreeDays[stepDuty] == 'x')
                    {
                         stepDuty++;

                        continue;
                    }

                   _workDiagram.WorkDiagramDayDriver[stepDuty] = 'x';
                   _driverDayPermanent[i].WorkDiagramDay[stepDuty] = 'K';
                   _driverDayPermanent[i].DriverHoursDay -= 1;
                   stepDuty += _gapDuty;

                    safetyCounter++;
                }
            }

        }
        public void AddExecutiveDays()
        {
            _executiveDayPermanent = new List<Worker>();

            foreach (var item in _workDiagram.PermanentWorkers)
            {
                if (item.ExecutiveHoursDay > 0)
                    _executiveDayPermanent.Add(item);
            }

            Random rand = new Random();

            for (int i = 0; i < _executiveDayPermanent.Count; i++)
            {
                int stepDuty = 0;

                /*Looking for first day duty */
                for (int j = 0; j < _executiveDayPermanent[i].WorkDiagramDay.Length; j++)
                {
                    if (_executiveDayPermanent[i].WorkDiagramDay[j] == 'K' || _executiveDayPermanent[i].WorkDiagramDay[j] == 'X')
                    {
                        stepDuty = j;
                        break;
                    }
                }

                int safetyCounter = 0;

                while (_executiveDayPermanent[i].ExecutiveHoursDay > 0)
                {
                    if (safetyCounter > loopSafety)
                    {
                        MessageBox.Show("SAFETY  EXIT");
                        break;
                    }

                    if (stepDuty > (_workDiagram.MonthDays - 1))
                    {
                        //  stepDuty -= (_workDiagram.MonthDays - 1);
                        stepDuty = 0;
                    }

                    if (_executiveDayPermanent[i].WorkDiagramDay[stepDuty] == 'K' || _executiveDayPermanent[i].WorkDiagramDay[stepDuty] == 'X')
                    {
                        stepDuty ++;
                        continue;
                    }


                    if (_workDiagram.WorkDiagramDayExecutive[stepDuty] == 'x' || _executiveDayPermanent[i].FreeDays[stepDuty] == 'x')
                    {
                        stepDuty++;

                        continue;
                    }

                    _workDiagram.WorkDiagramDayExecutive[stepDuty] = 'x';
                    _executiveDayPermanent[i].WorkDiagramDay[stepDuty] = 'X';
                    _executiveDayPermanent[i].ExecutiveHoursDay -= 1;
                    stepDuty += _gapDuty;

                    safetyCounter++;
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

            for (int i = 0; i < _driverNightPermanent.Count; i++)
            {
                int stepDuty = 0;

                /*Looking for first driver duty */
                for (int j = 0; j < _driverNightPermanent[i].WorkDiagramDay.Length; j++)
                {
                    if (_driverNightPermanent[i].WorkDiagramDay[j] == 'K' || _driverNightPermanent[i].WorkDiagramDay[j] == 'X')
                    {
                        stepDuty = j+1;
                        break;
                    }
                }

                int safetyCounter = 0;

                while (_driverNightPermanent[i].DriverHoursNight > 0)
                {
                    safetyCounter++;

                    if (safetyCounter > loopSafety)
                    {
                        MessageBox.Show("SAFETY  EXIT");
                        break;
                    }

                    if (stepDuty > (_workDiagram.MonthDays - 1))
                    {
                        // stepDuty -= (_workDiagram.MonthDays - 1);
                        stepDuty = 0;
                    }

                    /* check duty day */
                    if (_driverNightPermanent[i].WorkDiagramDay[stepDuty] == 'K' || _driverNightPermanent[i].WorkDiagramDay[stepDuty] == 'X')
                    {
                        stepDuty++;

                        continue;
                    }

                    /*check duty day tomorrow */
                    if (stepDuty < _workDiagram.MonthDays-1)
                    {
                        if (_driverNightPermanent[i].WorkDiagramDay[stepDuty+1] == 'K' || _driverNightPermanent[i].WorkDiagramDay[stepDuty+1] == 'X')
                        {
                            stepDuty++;
                            continue;
                        }
                    }

                    if (_workDiagram.WorkDiagramNightDriver[stepDuty] == 'x' 
                        || _driverNightPermanent[i].FreeDays[stepDuty] == 'x')
                    {

                        stepDuty++;

                        continue;
                    }

                    _workDiagram.WorkDiagramNightDriver[stepDuty] = 'x';
                    _driverNightPermanent[i].WorkDiagramNight[stepDuty] = 'L';
                    _driverNightPermanent[i].DriverHoursNight -= 1;
                    stepDuty += _gapDuty;

                }
            }

        }

      
        public void AddExecutiveNight()
        {
            _executiveNightPermanent = new List<Worker>();

            foreach (var item in _workDiagram.PermanentWorkers)
            {
                if (item.ExecutiveHoursNight> 0)
                    _executiveNightPermanent.Add(item);
            }

            Random rand = new Random();

            for (int i = 0; i < _executiveNightPermanent.Count; i++)
            {
                int stepDuty = rand.Next(10, _workDiagram.MonthDays - 5);

                /*Looking for first driver duty */
                for (int j = stepDuty; j < _executiveNightPermanent[i].WorkDiagramDay.Length; j++)
                {
                    if (_executiveNightPermanent[i].WorkDiagramNight[j] == 'L' || _executiveNightPermanent[i].WorkDiagramNight[j] == 'O')
                    {
                        stepDuty = j;
                        break;
                    }
                }

                int safetyCounter = 0;

                while (_executiveNightPermanent[i].ExecutiveHoursNight> 0)
                {
                    safetyCounter++;

                    if (safetyCounter > loopSafety)
                    {
                        MessageBox.Show("SAFETY  EXIT");
                        break;
                    }

                    if (stepDuty > (_workDiagram.MonthDays - 1))
                    {
                        //  stepDuty -= (_workDiagram.MonthDays - 1);
                        stepDuty = 0;

                    }

                    /* check duty night */
                    if (_executiveNightPermanent[i].WorkDiagramDay[stepDuty] == 'K' 
                        || _executiveNightPermanent[i].WorkDiagramDay[stepDuty] == 'X')
                    {
                        stepDuty++;

                        continue;
                    }

                    if ( _executiveNightPermanent[i].WorkDiagramNight[stepDuty] == 'O'
                        || _executiveNightPermanent[i].WorkDiagramNight[stepDuty] == 'L')
                    {
                        stepDuty++;
                        continue;
                    }

                    /*check duty day tomorrow */
                    if (stepDuty < _workDiagram.MonthDays - 1)
                    {
                        if (_executiveNightPermanent[i].WorkDiagramDay[stepDuty + 1] == 'K' || _executiveNightPermanent[i].WorkDiagramDay[stepDuty + 1] == 'X')
                        {
                            stepDuty++;
                            continue;
                        }
                    }

                    if (_workDiagram.WorkDiagramNightExecutive[stepDuty] == 'x'
                        || _executiveNightPermanent[i].FreeDays[stepDuty] == 'x')
                    {

                        stepDuty++;

                        continue;
                    }

                    _workDiagram.WorkDiagramNightExecutive[stepDuty] = 'x';
                    _executiveNightPermanent[i].WorkDiagramNight[stepDuty] = 'O';
                    _executiveNightPermanent[i].ExecutiveHoursNight -= 1;
                    stepDuty += _gapDuty;

                }
            }
        }

    }
}
