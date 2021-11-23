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
            Add24hDuty(workPlace);
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
                item.WorkDaysPerMonth = item.DriverDutyDay + item.DriverDutyNight + item.ExecutiveDutyDay + item.ExecutiveDutyNight;
                MessageBox.Show(item.WorkDaysPerMonth.ToString());
            }
        }

        private void Add24hDuty(string workPlace)
        {
            List<Worker> DD_EN = new List<Worker>();
            List<Worker> DN_ED = new List<Worker>();
            List<Worker> ED_EN = new List<Worker>();

            foreach (var item in _workDiagram.OtherAgreementWorkers)
            {
                if (item.WorkSystem == WorkSystem.FullDuty && item.WorkPlaceName == workPlace)
                {
                    if (item.DriverDutyDay > 0 && item.ExecutiveDutyNight > 0)
                    {
                        DD_EN.Add(item);
                        continue;
                    }

                    if (item.DriverDutyNight > 0 && item.ExecutiveDutyDay > 0)
                    {
                        DN_ED.Add(item);
                        continue;
                    }

                    if (item.ExecutiveDutyNight > 0 && item.ExecutiveDutyDay > 0)
                    {
                        ED_EN.Add(item);
                        continue;
                    }
                }
            }

            StartsWhenDriverDayIsEmpty(DD_EN);
            StartsWhenExecutiveDayIsEmpty(DN_ED);
            ExecutiveDayNightIsEmpty(ED_EN);           
        }

        private void StartsWhenDriverDayIsEmpty(List<Worker> dd_en)
        {
            bool isPossibleCreateDiagram = false;
            char[] tempEmptyDuties = new char[_workDiagram.MonthDays];

            /* Search DriverDays and ExecutiveNights */
            for (int i = 0; i < _workDiagram.MonthDays; i++)
            {
                if (_workDiagram.WorkDiagramDayDriver[i] != 'x' && _workDiagram.WorkDiagramNightExecutive[i] != 'x')
                {
                    tempEmptyDuties[i] = 'x';
                    isPossibleCreateDiagram = true;
                }
            }

            if (!isPossibleCreateDiagram) return;

            foreach (var item in dd_en)
            {

                /* Set _gapDuty */
                _gapDuty = 4;//_workDiagram.MonthDays / item.WorkDaysPerMonth;
                _stepDuty = 0;
                int safetyCounter = 0;

                /* Set 24h diagram */
                while (item.DriverDutyDay > 0 && item.ExecutiveDutyNight > 0)
                {
                    safetyCounter++;

                    /* safety loop exit */
                    if (safetyCounter > _loopSafety)
                    {
                        break;
                    }

                    if (_stepDuty > (_workDiagram.MonthDays - 1))
                    {
                        _stepDuty -= (_workDiagram.MonthDays);
                    }

                    for (int i = 0; i < tempEmptyDuties.Length; i++)
                    {
                        if (tempEmptyDuties[i] == 'x')
                        {
                            _stepDuty = i;
                            break;
                        }
                    }

                /*    if (item.WorkDiagramDay[_stepDuty] == 'K' || item.WorkDiagramDay[_stepDuty] == 'X'
                        || item.WorkDiagramNight[_stepDuty] == 'L' || item.WorkDiagramNight[_stepDuty] == 'O')
                    {
                        _stepDuty += 2;
                        continue;
                    }

                    if (_stepDuty > 0 && _stepDuty < _workDiagram.MonthDays-1)
                    {

                        if (item.WorkDiagramNight[_stepDuty-1] == 'L'|| item.WorkDiagramNight[_stepDuty - 1] == 'O'
                            || item.WorkDiagramDay[_stepDuty + 1] == 'K' || item.WorkDiagramDay[_stepDuty + 1] == 'X')
                        {
                            _stepDuty += 2;
                            continue;
                        } else
                        {
                            _stepDuty += 2;
                            continue;
                        }
                    } */

                    item.WorkDiagramDay[_stepDuty] = 'K';
                    item.WorkDiagramNight[_stepDuty] = 'O';
                    tempEmptyDuties[_stepDuty] = ' ';
                    _workDiagram.WorkDiagramDayDriver[_stepDuty] = 'x';
                    _workDiagram.WorkDiagramNightExecutive[_stepDuty] = 'x';
                    item.DriverDutyDay -= 1;
                    item.ExecutiveDutyDay -= 1;
                    _stepDuty += _gapDuty;
                    /* Tutaj bedzie logika biznesowa dodawania dób, uwzględnia:
                     * - dyżur przed _gapDuty
                     * - dyżur po _gapDuty
                     * - przkroczenie liczby dni
                     * - sprawdzenie czy dany dzień / noc nie jest zajęta
                     */
                }

            }
        }

        private void StartsWhenExecutiveDayIsEmpty(List<Worker> ed_en)
        {

            int loopIterator = 0;

            foreach (var item in ed_en)
            {

                bool isPossibleCreateDiagram = false;
                char[] tempEmptyDuties = new char[_workDiagram.MonthDays];

                /* Search ExecutiveNights and ExecutiveDays */
                for (int i = 0; i < _workDiagram.MonthDays; i++)
                {
                    if (_workDiagram.WorkDiagramNightExecutive[i] == ' ' && _workDiagram.WorkDiagramDayExecutive[i] == ' ')
                    {
                        tempEmptyDuties[i] = 'x';
                        isPossibleCreateDiagram = true;
                    }

                }

                if (!isPossibleCreateDiagram) return;

                /* Set _gapDuty */
                _gapDuty = ((_workDiagram.MonthDays / item.WorkDaysPerMonth) + 1) * 2;

                int safetyCounter = 0;

                /* Set 24h diagram */
                while (item.DriverDutyNight > 0 && item.ExecutiveDutyDay> 0)
                {
                    safetyCounter++;

                    /* safety loop exit */
                    if (safetyCounter > _loopSafety)
                    {
                        break;
                    }

                    if (_stepDuty > (_workDiagram.MonthDays - 1))
                    {
                        _stepDuty -= (_workDiagram.MonthDays);
                    }

                    for (int i = loopIterator; i < tempEmptyDuties.Length; i++)
                    {
                        if (tempEmptyDuties[i] == 'x')
                            _stepDuty = i;
                    }

                    if (item.WorkDiagramDay[_stepDuty] == 'K' || item.WorkDiagramDay[_stepDuty] == 'X'
                        || item.WorkDiagramNight[_stepDuty] == 'L' || item.WorkDiagramNight[_stepDuty] == 'O')
                    {
                        loopIterator = _stepDuty + 1;
                    }

                    if (_stepDuty > 0 && _stepDuty < _workDiagram.MonthDays)
                    {

                        if (item.WorkDiagramNight[_stepDuty - 1] != 'L' && item.WorkDiagramNight[_stepDuty - 1] != 'O'
                            && item.WorkDiagramDay[_stepDuty + 1] != 'K' && item.WorkDiagramDay[_stepDuty + 1] != 'X')
                        {
                            item.WorkDiagramDay[_stepDuty] = 'X';
                            item.WorkDiagramNight[_stepDuty] = 'L';
                            tempEmptyDuties[_stepDuty] = ' ';
                            _workDiagram.WorkDiagramNightDriver[_stepDuty] = 'x';
                            _workDiagram.WorkDiagramDayExecutive[_stepDuty] = 'x';
                            item.DriverDutyNight -= 1;
                            item.ExecutiveDutyDay -= 1;
                            loopIterator = _stepDuty + _gapDuty;
                        }
                        else
                        {
                            loopIterator = _stepDuty + 1;
                        }

                    }
                    /* Tutaj bedzie logika biznesowa dodawania dób, uwzględnia:
                     * - dyżur przed _gapDuty
                     * - dyżur po _gapDuty
                     * - przkroczenie liczby dni
                     * - sprawdzenie czy dany dzień / noc nie jest zajęta
                     */
                }

            }
        }

        private void ExecutiveDayNightIsEmpty(List<Worker> dn_ed)
        {

            int loopIterator = 0;

            foreach (var item in dn_ed)
            {

                bool isPossibleCreateDiagram = false;
                char[] tempEmptyDuties = new char[_workDiagram.MonthDays];

                /* Search DriverNights and ExecutiveDays */
                for (int i = 0; i < _workDiagram.MonthDays; i++)
                {
                    if (_workDiagram.WorkDiagramNightDriver[i] == ' ' && _workDiagram.WorkDiagramDayExecutive[i] == ' ')
                    {
                        tempEmptyDuties[i] = 'x';
                        isPossibleCreateDiagram = true;
                    }

                }

                if (!isPossibleCreateDiagram) return;

                /* Set _gapDuty */
                _gapDuty = ((_workDiagram.MonthDays / item.WorkDaysPerMonth) + 1) * 2;

                int safetyCounter = 0;

                /* Set 24h diagram */
                while (item.ExecutiveDutyNight > 0 && item.ExecutiveDutyDay > 0)
                {
                    safetyCounter++;

                    /* safety loop exit */
                    if (safetyCounter > _loopSafety)
                    {
                        break;
                    }

                    if (_stepDuty > (_workDiagram.MonthDays - 1))
                    {
                        _stepDuty -= (_workDiagram.MonthDays);
                    }

                    for (int i = loopIterator; i < tempEmptyDuties.Length; i++)
                    {
                        if (tempEmptyDuties[i] == 'x')
                            _stepDuty = i;
                    }

                    if (item.WorkDiagramDay[_stepDuty] == 'K' || item.WorkDiagramDay[_stepDuty] == 'X'
                        || item.WorkDiagramNight[_stepDuty] == 'L' || item.WorkDiagramNight[_stepDuty] == 'O')
                    {
                        loopIterator = _stepDuty + 1;
                    }

                    if (_stepDuty > 0 && _stepDuty < _workDiagram.MonthDays)
                    {

                        if (item.WorkDiagramNight[_stepDuty - 1] != 'L' && item.WorkDiagramNight[_stepDuty - 1] != 'O'
                            && item.WorkDiagramDay[_stepDuty + 1] != 'K' && item.WorkDiagramDay[_stepDuty + 1] != 'X')
                        {
                            item.WorkDiagramDay[_stepDuty] = 'X';
                            item.WorkDiagramNight[_stepDuty] = 'O';
                            tempEmptyDuties[_stepDuty] = ' ';
                            _workDiagram.WorkDiagramNightExecutive[_stepDuty] = 'x';
                            _workDiagram.WorkDiagramDayExecutive[_stepDuty] = 'x';
                            loopIterator = _stepDuty + _gapDuty;
                        }
                        else
                        {
                            loopIterator = _stepDuty + 1;
                        }

                    }
                    /* Tutaj bedzie logika biznesowa dodawania dób, uwzględnia:
                     * - dyżur przed _gapDuty
                     * - dyżur po _gapDuty
                     * - przkroczenie liczby dni
                     * - sprawdzenie czy dany dzień / noc nie jest zajęta
                     */
                }

            }
        }

        private void AddDriverDays(string workPlace)
        {
            _dayDuties = new List<Worker>();

            foreach (var item in _workDiagram.OtherAgreementWorkers)
            {
                if (item.DriverDutyDay > 0 && item.WorkPlaceName == workPlace)
                    _dayDuties.Add(item);
            }

        }

        private void AddExecutiveDays(string workPlace)
        {
            _executiveDay = new List<Worker>();

            foreach (var item in _workDiagram.OtherAgreementWorkers)
            {
                if (item.DriverDutyNight > 0 && item.WorkPlaceName == workPlace)
                    _executiveDay.Add(item);
            }

        }

        private void AddDriverNight(string workPlace)
        {
            _nightDuties = new List<Worker>();

            foreach (var item in _workDiagram.OtherAgreementWorkers)
            {
                if (item.ExecutiveDutyDay > 0 && item.WorkPlaceName == workPlace)
                    _nightDuties.Add(item);
            }

        }

        private void AddExecutiveNight(string workPlace)
        {
            _executiveNight = new List<Worker>();

            foreach (var item in _workDiagram.OtherAgreementWorkers)
            {
                if (item.ExecutiveDutyNight > 0 && item.WorkPlaceName == workPlace)
                    _executiveNight.Add(item);

            }
        }
    }
}
