using System;
using System.Collections.Generic;

namespace Grafik
{
    public class WorkDiagram
    {
        private char[] _workDiagramDayDriver;
        private char[] _workDiagramNightDriver;
        private char[] _workDiagramDayExecutive;
        private char[] _workDiagramNightExecutive;
        private string _monthName;
        private int _monthDays;

        private List<Worker> _permanentWorkers;
        private List<Worker> _otherAgreementWorkers;

        public WorkDiagram(int monthDays)
        {
            _workDiagramDayDriver = new char[monthDays];
            _workDiagramNightDriver = new char[monthDays];
            _workDiagramDayExecutive = new char[monthDays];
            _workDiagramNightExecutive = new char[monthDays];
            _monthDays = monthDays;

            _permanentWorkers = new List<Worker>();
            _otherAgreementWorkers = new List<Worker>();
        }

        public List<Worker> PermanentWorkers { get => _permanentWorkers; set => _permanentWorkers = value; }
        public List<Worker> OtherAgreementWorkers { get => _otherAgreementWorkers; set => _otherAgreementWorkers = value; }
        public string MonthName { get => _monthName; set => _monthName = value; }
        public int MonthDays { get => _monthDays; set => _monthDays = value; }
        public char[] WorkDiagramDayDriver { get => _workDiagramDayDriver; set => _workDiagramDayDriver = value; }
        public char[] WorkDiagramNightDriver { get => _workDiagramNightDriver; set => _workDiagramNightDriver = value; }
        public char[] WorkDiagramDayExecutive { get => _workDiagramDayExecutive; set => _workDiagramDayExecutive = value; }
        public char[] WorkDiagramNightExecutive { get => _workDiagramNightExecutive; set => _workDiagramNightExecutive = value; }
    }
}
