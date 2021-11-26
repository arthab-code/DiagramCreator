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

        public override void Create(string workPlace)
        {
            AddDayDuties(workPlace);
            AddNightDuties(workPlace);

        }
        /* Stworzenie wersji z wyszukiwaniem najblizszych dyzurow z tablic ogolnego grafiku _workDiagram */

        private void AddDayDuties(string workPlace)
        {
            _dayDuties = new List<Worker>();
            /* DUTY DAY SETTING LIST */
            foreach (var item in _workDiagram.PermanentWorkers)
            {
                if ((item.DriverDutyDay > 0 || item.ExecutiveDutyDay > 0) && item.WorkPlaceName == workPlace)
                    _dayDuties.Add(item);
            }

            Random rand = new Random();


            /* Set driver day diagram */
            foreach (var item in _dayDuties)
            {
                _stepDuty =  rand.Next(0, 11);
                while (item.DriverDutyDay > 0 || item.ExecutiveDutyDay > 0)
                {
                    CheckAndCorrectStepDutyRange();

                    /* Driver day setter */
                    if (item.DriverDutyDay > 0)
                    {
                        if (_workDiagram.WorkDiagramDayDriver[_stepDuty] != 'x')
                        {
                            if (item.WorkDiagramDay[_stepDuty] != 'K' && item.WorkDiagramDay[_stepDuty] != 'X' && item.FreeDays[_stepDuty] != 'x')
                            {
                                _workDiagram.WorkDiagramDayDriver[_stepDuty] = 'x';
                                item.WorkDiagramDay[_stepDuty] = 'K';
                                _stepDuty += _gapDuty;
                                item.DriverDutyDay -= 1;
                            }
                            else
                            {
                                _stepDuty++;
                            }
                        }
                        else
                        {
                            _stepDuty++;
                        }
                    }
                    CheckAndCorrectStepDutyRange();

                    if (item.DriverDutyDay <= 0)
                    {

                        /* Executive day setter */
                        if (_workDiagram.WorkDiagramDayExecutive[_stepDuty] != 'x')
                        {
                            if (item.WorkDiagramDay[_stepDuty] != 'K' && item.WorkDiagramDay[_stepDuty] != 'X' && item.FreeDays[_stepDuty] != 'x')
                            {
                                _workDiagram.WorkDiagramDayExecutive[_stepDuty] = 'x';
                                item.WorkDiagramDay[_stepDuty] = 'X';
                                _stepDuty += _gapDuty;
                                item.ExecutiveDutyDay -= 1;
                            } 
                            else
                            {
                                _stepDuty++;
                            }
                        }
                        else
                        {
                            _stepDuty++;
                        }

                    }
                }

            }
        }

        private void AddNightDuties(string workPlace)
        {
            _nightDuties = new List<Worker>();
            /* DUTY Night SETTING LIST */
            foreach (var item in _workDiagram.PermanentWorkers)
            {
                if ((item.DriverDutyNight > 0 || item.ExecutiveDutyNight > 0) && item.WorkPlaceName == workPlace)
                    _nightDuties.Add(item);
            }

            /* Set driver night diagram */
            foreach (var item in _nightDuties)
            {

                /* Search first day in month */
                for (int i = _stepDuty; i < _workDiagram.MonthDays; i++)
                {
                    if (item.WorkDiagramDay[i] == 'K' || item.WorkDiagramDay[i] == 'X')
                        _stepDuty = i + 1;
                }
                int safetyCounter = 0;

                while (item.DriverDutyNight > 0 || item.ExecutiveDutyNight > 0)
                {
                    safetyCounter++;
                    if (safetyCounter > 5000)
                    {
                        MessageBox.Show("Nie do konca uzupelniony grafik - blad");
                        break;
                    }

                    CheckAndCorrectStepDutyRange();

                    /* Driver nitght setter */
                    if (item.DriverDutyNight > 0)
                    {
                        if (_workDiagram.WorkDiagramNightDriver[_stepDuty] != 'x')
                        {
                            if (item.WorkDiagramNight[_stepDuty] != 'L' && item.WorkDiagramNight[_stepDuty] != 'O' && item.FreeDays[_stepDuty] != 'x')
                            {

                                /* Check this same day duty */
                                if (item.WorkDiagramDay[_stepDuty] == 'K' || item.WorkDiagramDay[_stepDuty] == 'X')
                                {
                                    _stepDuty++;
                                    continue;
                                }

                                /* Check tommorow that worker have day duty */
                                if (_stepDuty < _workDiagram.MonthDays - 1)
                                {
                                    if (item.WorkDiagramDay[_stepDuty + 1] == 'K' || item.WorkDiagramDay[_stepDuty + 1] == 'X')
                                    {
                                        _stepDuty++;
                                        continue;
                                    }
                                }


                                _workDiagram.WorkDiagramNightDriver[_stepDuty] = 'x';
                                item.WorkDiagramNight[_stepDuty] = 'L';
                                _stepDuty += _gapDuty;
                                item.DriverDutyNight -= 1;
                            } else
                            {
                                _stepDuty+=_gapDuty;
                            }
                        } else
                        {
                            _stepDuty++;
                        }

                    }
                    CheckAndCorrectStepDutyRange();

                    if (item.DriverDutyNight <= 0)
                    {

                        /* Executive day setter */
                        if (_workDiagram.WorkDiagramNightExecutive[_stepDuty] != 'x')
                        {
                            /* Check this same day duty */
                            if (item.WorkDiagramDay[_stepDuty] == 'K' || item.WorkDiagramDay[_stepDuty] == 'X')
                            {
                                _stepDuty++;
                                continue;
                            }

                            if (item.WorkDiagramNight[_stepDuty] != 'L' && item.WorkDiagramNight[_stepDuty] != 'O' && item.FreeDays[_stepDuty] != 'x')
                            {
                                /* Check tommorow that worker have day duty */
                                if (_stepDuty < _workDiagram.MonthDays - 1)
                                {
                                    if (item.WorkDiagramDay[_stepDuty + 1] == 'K' || item.WorkDiagramDay[_stepDuty + 1] == 'X')
                                    {
                                        _stepDuty++;
                                        continue;
                                    }
                                }


                                _workDiagram.WorkDiagramNightExecutive[_stepDuty] = 'x';
                                item.WorkDiagramNight[_stepDuty] = 'O';
                                _stepDuty += _gapDuty;
                                item.ExecutiveDutyNight -= 1;
                            }
                            else
                            {
                                _stepDuty+=_gapDuty;
                            }

                        } else
                        {
                            _stepDuty++;
                        }
                    }

                }
            }
        }

    }     
    
}
