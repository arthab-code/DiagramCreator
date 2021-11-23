using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public abstract class DiagramCreator
    {
        protected WorkDiagram _workDiagram;
        protected List<Worker> _dayDuties;
        protected List<Worker> _nightDuties;
        protected List<Worker> _executiveDay;
        protected List<Worker> _executiveNight;
        protected int _gapDuty;
        protected int _loopSafety;
        protected int _stepDuty;

        public List<Worker> DayDutiesPermanent { get => _dayDuties; set => _dayDuties = value; }
        public WorkDiagram WorkDiagram { get => _workDiagram; set => _workDiagram = value; }
        public List<Worker> DriverNight { get => _nightDuties; set => _nightDuties = value; }
        public List<Worker> ExecutiveDay { get => _executiveDay; set => _executiveDay = value; }
        public List<Worker> ExecutiveNight { get => _executiveNight; set => _executiveNight = value; }

        public abstract void Create(string workPlace);
    }
}
