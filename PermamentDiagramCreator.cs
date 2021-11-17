using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Grafik
{
    public class PermamentDiagramCreator : DiagramCreator
    {
        public PermamentDiagramCreator(WorkDiagram workDiagram)
        {
            _workDiagram = workDiagram;
            _gapDuty = 4;
            _stepDuty = 0;
            _loopSafety = 1000;

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

        public override void Create(string workPlace)
        {
            AddDriverDays(workPlace);
            AddExecutiveDays(workPlace);
            AddDriverNight(workPlace);
            AddExecutiveNight(workPlace);
        }
        /* Stworzenie wersji z wyszukiwaniem najblizszych dyzurow z tablic ogolnego grafiku _workDiagram */

        private void AddDriverDays(string workPlace)
        {
           _driverDay = new List<Worker>();

            foreach (var item in _workDiagram.PermanentWorkers)
            {
                if (item.DriverHoursDay > 0 && item.WorkPlaceName == workPlace)
                    _driverDay.Add(item);
                
            }
            Random rand = new Random();

            _stepDuty = rand.Next(0, _workDiagram.MonthDays);

            for (int i = 0; i < _driverDay.Count; i++)
            {
                int safetyCounter = 0;
                
                while (_driverDay[i].DriverHoursDay > 0)
                {
                    safetyCounter++;

                    if (safetyCounter > _loopSafety)
                    {
                        MessageBox.Show("SAFETY  EXIT");
                        break;
                    }

                    if (_stepDuty > (_workDiagram.MonthDays-1))
                    {
                        _stepDuty -= (_workDiagram.MonthDays);
                        //stepDuty = 0;
                    }
                    
                    if (_workDiagram.WorkDiagramDayDriver[_stepDuty] == 'x' || _driverDay[i].FreeDays[_stepDuty] == 'x')
                    {
                        _stepDuty++;

                        continue;
                    }

                   _workDiagram.WorkDiagramDayDriver[_stepDuty] = 'x';
                   _driverDay[i].WorkDiagramDay[_stepDuty] = 'K';
                   _driverDay[i].DriverHoursDay -= 1;
                   _stepDuty += _gapDuty;

                }
            }

        }
        private void AddExecutiveDays(string workPlace)
        {
            _executiveDay = new List<Worker>();

            foreach (var item in _workDiagram.PermanentWorkers)
            {
                if (item.ExecutiveHoursDay > 0 && item.WorkPlaceName == workPlace)
                    _executiveDay.Add(item);
            }

            Random rand = new Random();

            for (int i = 0; i < _executiveDay.Count; i++)
            {
                /*Looking for first day duty */
                for (int j = 0; j < _executiveDay[i].WorkDiagramDay.Length; j++)
                {
                    if (_executiveDay[i].WorkDiagramDay[j] == 'K')
                    {
                        _stepDuty = j;
                        break;
                    }
                }

                int safetyCounter = 0;

                while (_executiveDay[i].ExecutiveHoursDay > 0)
                {
                    safetyCounter++;

                    if (safetyCounter > _loopSafety)
                    {
                        MessageBox.Show("SAFETY  EXIT");
                        break;
                    }

                    if (_stepDuty > (_workDiagram.MonthDays - 1))
                    {
                        _stepDuty -= (_workDiagram.MonthDays);
                      //  stepDuty = 0;
                    }

                    if (_executiveDay[i].WorkDiagramDay[_stepDuty] == 'K' || _executiveDay[i].WorkDiagramDay[_stepDuty] == 'X')
                    {
                        _stepDuty += _gapDuty;
                        continue;
                    }


                    if (_workDiagram.WorkDiagramDayExecutive[_stepDuty] == 'x' || _executiveDay[i].FreeDays[_stepDuty] == 'x')
                    {
                        _stepDuty++;

                        continue;
                    }

                    _workDiagram.WorkDiagramDayExecutive[_stepDuty] = 'x';
                    _executiveDay[i].WorkDiagramDay[_stepDuty] = 'X';
                    _executiveDay[i].ExecutiveHoursDay -= 1;
                    _stepDuty += _gapDuty;

                }
            }
        }

        private void AddDriverNight(string workPlace)
        {
            _driverNight = new List<Worker>();

            foreach (var item in _workDiagram.PermanentWorkers)
            {
                if (item.DriverHoursNight > 0 && item.WorkPlaceName == workPlace)
                    _driverNight.Add(item);
            }

            Random rand = new Random();

            for (int i = 0; i < _driverNight.Count; i++)
            {

                /*Looking for first driver duty */
                for (int j = 0; j < _driverNight[i].WorkDiagramDay.Length; j++)
                {
                    if (_driverNight[i].WorkDiagramDay[j] == 'K' || _driverNight[i].WorkDiagramDay[j] == 'X')
                    {
                        _stepDuty = j+1;
                        break;
                    }
                }

                int safetyCounter = 0;

                while (_driverNight[i].DriverHoursNight > 0)
                {
                    safetyCounter++;

                    if (safetyCounter > _loopSafety)
                    {
                        MessageBox.Show("SAFETY  EXIT");
                        break;
                    }

                    if (_stepDuty > (_workDiagram.MonthDays - 1))
                    {
                        _stepDuty -= (_workDiagram.MonthDays);
                        //stepDuty = 0;
                    }

                    /* check duty day */
                    if (_driverNight[i].WorkDiagramDay[_stepDuty] == 'K' || _driverNight[i].WorkDiagramDay[_stepDuty] == 'X')
                    {
                        _stepDuty++;

                        continue;
                    }

                    /*check duty day tomorrow */
                    if (_stepDuty < _workDiagram.MonthDays-1)
                    {
                        if (_driverNight[i].WorkDiagramDay[_stepDuty+1] == 'K' || _driverNight[i].WorkDiagramDay[_stepDuty+1] == 'X')
                        {
                            _stepDuty++;
                            continue;
                        }
                    }

                    if (_driverNight[i].WorkDiagramNight[_stepDuty] == 'O'
                        || _driverNight[i].WorkDiagramNight[_stepDuty] == 'L')
                    {
                        _stepDuty += _gapDuty;
                        continue;
                    }

                    if (_workDiagram.WorkDiagramNightDriver[_stepDuty] == 'x' 
                        || _driverNight[i].FreeDays[_stepDuty] == 'x')
                    {

                        _stepDuty++;

                        continue;
                    }

                    _workDiagram.WorkDiagramNightDriver[_stepDuty] = 'x';
                    _driverNight[i].WorkDiagramNight[_stepDuty] = 'L';
                    _driverNight[i].DriverHoursNight -= 1;
                    _stepDuty += _gapDuty;

                }
            }

        }

      
        private void AddExecutiveNight(string workPlace)
        {
            _executiveNight = new List<Worker>();

            foreach (var item in _workDiagram.PermanentWorkers)
            {
                if (item.ExecutiveHoursNight> 0 && item.WorkPlaceName == workPlace)
                    _executiveNight.Add(item);
            }

            Random rand = new Random();

            for (int i = 0; i < _executiveNight.Count; i++)
            {

                /*Looking for first driver duty */
                for (int j = 0; j < _executiveNight[i].WorkDiagramDay.Length; j++)
                {
                    if (_executiveNight[i].WorkDiagramDay[j] == 'K' || _executiveNight[i].WorkDiagramDay[j] == 'X')
                    {
                        _stepDuty = j+1;
                        break;
                    }
                }

                int safetyCounter = 0;

                while (_executiveNight[i].ExecutiveHoursNight> 0)
                {
                    safetyCounter++;

                    if (safetyCounter > _loopSafety)
                    {
                        MessageBox.Show("SAFETY  EXIT");
                        break;
                    }

                    if (_stepDuty > (_workDiagram.MonthDays - 1))
                    {
                        _stepDuty -= (_workDiagram.MonthDays);
                      //  stepDuty = 0;

                    }

                    if (_executiveNight[i].WorkDiagramNight[_stepDuty] == 'O'
                       || _executiveNight[i].WorkDiagramNight[_stepDuty] == 'L')
                    {
                        _stepDuty += _gapDuty;
                        continue;
                    }

                    /* check duty night */
                    if (_executiveNight[i].WorkDiagramDay[_stepDuty] == 'K' 
                        || _executiveNight[i].WorkDiagramDay[_stepDuty] == 'X')
                    {
                        _stepDuty++;

                        continue;
                    }

                    /*check duty day tomorrow */
                    if (_stepDuty < _workDiagram.MonthDays - 1)
                    {
                        if (_executiveNight[i].WorkDiagramDay[_stepDuty + 1] == 'K' || _executiveNight[i].WorkDiagramDay[_stepDuty + 1] == 'X')
                        {
                            _stepDuty++;
                            continue;
                        }
                        if (_executiveNight[i].WorkDiagramNight[_stepDuty + 1] == 'L' || _executiveNight[i].WorkDiagramNight[_stepDuty + 1] == 'O')
                        {
                            _stepDuty += _gapDuty;
                            continue;
                        }
                    }

                    if (_workDiagram.WorkDiagramNightExecutive[_stepDuty] == 'x'
                        || _executiveNight[i].FreeDays[_stepDuty] == 'x')
                    {

                        _stepDuty++;

                        continue;
                    }

                    _workDiagram.WorkDiagramNightExecutive[_stepDuty] = 'x';
                    _executiveNight[i].WorkDiagramNight[_stepDuty] = 'O';
                    _executiveNight[i].ExecutiveHoursNight -= 1;
                    _stepDuty += _gapDuty;

                }
            }
        }

    }
}
