using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

            CreateWorkerDiagramArray(_workDiagram.MonthDays);
        }

        public override void Create(string workPlace)
        {
            AddDayDuties(workPlace);
            AddNightDuties(workPlace);
        }

        private void CreateWorkerDiagramArray(int monthDays)
        {
            foreach (var item in _workDiagram.OtherAgreementWorkers)
            {
                item.WorkDiagramDay = new char[monthDays];
                item.WorkDiagramNight = new char[monthDays];
                item.WorkDaysPerMonth = item.DriverDutyDay + item.DriverDutyNight + item.ExecutiveDutyDay + item.ExecutiveDutyNight;
            }
        }

        

        private void AddDayDuties(string workPlace)
        {
            _dayDuties = new List<Worker>();

            foreach (var item in _workDiagram.OtherAgreementWorkers)
            {
                if ((item.DriverDutyDay > 0 || item.ExecutiveDutyDay > 0) && item.WorkPlaceName == workPlace)
                    _dayDuties.Add(item);
            }         

           /* Driver day setter */
            for (int i = 0; i < _workDiagram.MonthDays; i++)
            {
                if (_workDiagram.WorkDiagramDayDriver[i] != 'x')
                {
                    for (int j = 0; j < _dayDuties.Count; j++)
                    {
                        if (_dayDuties[j].FreeDays[i] == 'x')
                            continue;

                        if (_dayDuties[j].DriverDutyDay <= 0)
                            continue;

                        if (_dayDuties[j].WorkDiagramDay[i] == 'K' || _dayDuties[j].WorkDiagramDay[i] == 'X')
                            continue;

                        if (_workDiagram.WorkDiagramDayDriver[i] == 'x')
                        {
                            i++;
                            if (i >= _workDiagram.MonthDays)
                                i = 0;
                            continue;
                        }

                        _dayDuties[j].WorkDiagramDay[i] = 'K';
                        _dayDuties[j].DriverDutyDay -= 1;
                        _workDiagram.WorkDiagramDayDriver[i] = 'x';
                    }
                }
            }

        

            /*
             * DO POPRAWY USTAWIANIE 48 -- , BLAD Z PRACOWNIKIEM KONTRAKTOWYM (?) JAKO KIEROWCĄ
             * 
             */

            /* Executive day setter */
            for (int i = 0; i < _workDiagram.MonthDays; i++)
            {
                if (_workDiagram.WorkDiagramDayExecutive[i] != 'x')
                {
                    for (int j = 0; j < _dayDuties.Count; j++)
                    {
                        if (_dayDuties[j].FreeDays[i] == 'x')
                            continue;

                        if (_dayDuties[j].ExecutiveDutyDay <= 0)
                            continue;

                        if (_dayDuties[j].WorkDiagramDay[i] == 'K' || _dayDuties[j].WorkDiagramDay[i] == 'X')
                            continue;

                        if (_workDiagram.WorkDiagramDayExecutive[i] == 'x')
                        {
                            i++;
                            if (i >= _workDiagram.MonthDays)
                                i = 0;
                            continue;
                        }

                        _dayDuties[j].WorkDiagramDay[i] = 'X';
                            _dayDuties[j].ExecutiveDutyDay -= 1;
                            _workDiagram.WorkDiagramDayExecutive[i] = 'x';       
                    }
                }

            } 


        }

        private void AddNightDuties(string workPlace)
        {
            _nightDuties = new List<Worker>();

            foreach (var item in _workDiagram.OtherAgreementWorkers)
            {
                if ((item.DriverDutyNight > 0 || item.ExecutiveDutyNight > 0) && item.WorkPlaceName == workPlace)
                    _nightDuties.Add(item);
            }

            /* DIAGRAM CREATOR FOR 24h WORKERS */

            /* Driver night setter for 24h workers with -X-L- configuration*/
            for (int i = 0; i < _workDiagram.MonthDays; i++)
            {
                if (_workDiagram.WorkDiagramNightDriver[i] != 'x')
                {
                    for (int j = 0; j < _nightDuties.Count; j++)
                    {
                        if (_nightDuties[j].WorkSystem != WorkSystem.FullDuty) continue;

                        if (_workDiagram.WorkDiagramNightDriver[i] == 'x')
                        {
                            i++;
                            if (i >= _workDiagram.MonthDays)
                                i = 0;
                            continue;
                        }

                        if (_nightDuties[j].DriverDutyNight <= 0) continue;

                        if (i < _workDiagram.MonthDays - 1)
                        {
                            if (_nightDuties[j].WorkDiagramDay[i + 1] == 'K' || _nightDuties[j].WorkDiagramDay[i + 1] == 'X')
                                continue;
                        }

                        if (_nightDuties[j].WorkDiagramDay[i] == 'X')
                        {
                            _workDiagram.WorkDiagramNightDriver[i] = 'x';
                            _nightDuties[j].DriverDutyNight -= 1;
                            _nightDuties[j].WorkDiagramNight[i] = 'L';

                        }
                        
                    }
                }

            }

            /* Driver night setter for 24h workers with -K-O- configuration*/
            for (int i = 0; i < _workDiagram.MonthDays; i++)
            {
                if (_workDiagram.WorkDiagramNightExecutive[i] != 'x')
                {
                    for (int j = 0; j < _nightDuties.Count; j++)
                    {
                        if (_nightDuties[j].WorkSystem != WorkSystem.FullDuty) continue;

                        if (_workDiagram.WorkDiagramNightExecutive[i] == 'x')
                        {
                            i++;

                            if (i >= _workDiagram.MonthDays)
                                i = 0;

                            continue;
                        }

                        if (_nightDuties[j].ExecutiveDutyNight <= 0) continue;

                        if (i < _workDiagram.MonthDays-1)
                        {
                            if (_nightDuties[j].WorkDiagramDay[i + 1] == 'K' || _nightDuties[j].WorkDiagramDay[i + 1] == 'X')
                                continue;
                        }


                        if (_nightDuties[j].WorkDiagramDay[i] == 'K')
                        {
                            _workDiagram.WorkDiagramNightExecutive[i] = 'x';
                            _nightDuties[j].ExecutiveDutyNight -= 1;
                            _nightDuties[j].WorkDiagramNight[i] = 'O';

                        }

                    }
                }

            }

            /* Driver night setter for 24h workers with -X-O- configuration*/
            for (int i = 0; i < _workDiagram.MonthDays; i++)
            {
                if (_workDiagram.WorkDiagramNightExecutive[i] != 'x')
                {
                    for (int j = 0; j < _nightDuties.Count; j++)
                    {
                        if (_nightDuties[j].WorkSystem != WorkSystem.FullDuty) continue;

                        if (_workDiagram.WorkDiagramNightExecutive[i] == 'x')
                        {
                            i++;
                            if (i >= _workDiagram.MonthDays)
                                i = 0;
                            continue;
                        }

                        if (_nightDuties[j].ExecutiveDutyNight <= 0) continue;

                        if (i < _workDiagram.MonthDays - 1)
                        {
                            if (_nightDuties[j].WorkDiagramDay[i + 1] == 'K' || _nightDuties[j].WorkDiagramDay[i + 1] == 'X')
                                continue;
                        }


                        if (_nightDuties[j].WorkDiagramDay[i] == 'X')
                        {
                            _workDiagram.WorkDiagramNightExecutive[i] = 'x';
                            _nightDuties[j].ExecutiveDutyNight -= 1;
                            _nightDuties[j].WorkDiagramNight[i] = 'O';

                        }

                    }
                }

            }
            /* END FOR 24h WORKERS */

            /* DIAGRAM CREATOR FOR 12h WORKERS */
            /* Driver night setter for 12h workers */
            for (int i = 0; i < _workDiagram.MonthDays; i++)
            {
                if (_workDiagram.WorkDiagramNightDriver[i] != 'x')
                {
                    for (int j = 0; j < _nightDuties.Count; j++)
                    {
                        if (_dayDuties[j].FreeDays[i] == 'x')
                            continue;

                        if (_workDiagram.WorkDiagramNightDriver[i] == 'x')
                        {
                            i++;
                            if (i >= _workDiagram.MonthDays)
                                i = 0;
                            continue;
                        }

                        if (_nightDuties[j].DriverDutyNight <= 0) continue;

                        if (i < _workDiagram.MonthDays - 1)
                        {
                            if (_nightDuties[j].WorkDiagramDay[i + 1] == 'K' || _nightDuties[j].WorkDiagramDay[i + 1] == 'X')
                                continue;
                        }

                        if (_nightDuties[j].WorkDiagramDay[i] == 'K' || _nightDuties[j].WorkDiagramDay[i] == 'X'
                            || _nightDuties[j].WorkDiagramNight[i] == 'L' || _nightDuties[j].WorkDiagramNight[i] == 'O')
                        {
                            continue;
                        }

                        _workDiagram.WorkDiagramNightDriver[i] = 'x';
                        _nightDuties[j].DriverDutyNight -= 1;
                        _nightDuties[j].WorkDiagramNight[i] = 'L';
                    }
                }
            }
                /* Executive night setter for 12h workers */
                for (int i = 0; i < _workDiagram.MonthDays; i++)
                {
                    if (_workDiagram.WorkDiagramNightExecutive[i] != 'x')
                    {
                        for (int j = 0; j < _nightDuties.Count; j++)
                        {

                        if (_dayDuties[j].FreeDays[i] == 'x')
                            continue;

                        if (_workDiagram.WorkDiagramNightExecutive[i] == 'x')
                        {
                            i++;
                            if (i >= _workDiagram.MonthDays)
                                i = 0;
                            continue;
                        }

                        if (_nightDuties[j].ExecutiveDutyNight <= 0) continue;

                            if (i < _workDiagram.MonthDays - 1)
                            {
                                if (_nightDuties[j].WorkDiagramDay[i + 1] == 'K' || _nightDuties[j].WorkDiagramDay[i + 1] == 'X')
                                    continue;
                            }

                            if (_nightDuties[j].WorkDiagramDay[i] == 'K' || _nightDuties[j].WorkDiagramDay[i] == 'X'
                                || _nightDuties[j].WorkDiagramNight[i] == 'L' || _nightDuties[j].WorkDiagramNight[i] == 'O')
                            {
                                continue;
                            }

                            _workDiagram.WorkDiagramNightExecutive[i] = 'x';
                            _nightDuties[j].ExecutiveDutyNight -= 1;
                            _nightDuties[j].WorkDiagramNight[i] = 'O';
                        }
                    }

                }
            
        }

    }
}

