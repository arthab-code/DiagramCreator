using System;
using System.Collections.Generic;

namespace Grafik
{
    public class WorkDiagram
    {
        private char[,] _workFullDiagram;
        private List<Worker> _permanentWorkers;
        private List<Worker> _otherAgreementWorkers;

        public WorkDiagram(int monthDays)
        {
            _workFullDiagram = new char[4, monthDays];

            _permanentWorkers = new List<Worker>();
            _otherAgreementWorkers = new List<Worker>();
        }

        public char[,] WorkFullDiagram 
        { 
            get => _workFullDiagram; 
            set => _workFullDiagram = value; 
        }
    }
}
