using System;
using System.Collections.Generic;

namespace Grafik
{
    public class WorkDiagram
    {
        private char[,] _workFullDiagram;
        private string _monthName;
        private int _monthDays;

        private List<Worker> _permanentWorkers;
        private List<Worker> _otherAgreementWorkers;

        public WorkDiagram(int workerNumber, int monthDays)
        {
            _workFullDiagram = new char[workerNumber*2, monthDays];
            _monthDays = monthDays;

            _permanentWorkers = new List<Worker>();
            _otherAgreementWorkers = new List<Worker>();
        }

        public char[,] WorkFullDiagram 
        { 
            get => _workFullDiagram; 
            set => _workFullDiagram = value; 
        }
        public List<Worker> PermanentWorkers { get => _permanentWorkers; set => _permanentWorkers = value; }
        public List<Worker> OtherAgreementWorkers { get => _otherAgreementWorkers; set => _otherAgreementWorkers = value; }
        public string MonthName { get => _monthName; set => _monthName = value; }
        public int MonthDays { get => _monthDays; set => _monthDays = value; }
    }
}
