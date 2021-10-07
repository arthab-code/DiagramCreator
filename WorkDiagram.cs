using System;
using System.Collections.Generic;

namespace Grafik
{
    public class WorkDiagram
    {
        private char[] _shift;
        private Dictionary<byte, char[]> _diagram;  // key = number of day || value = array of shift -> day or night
        private int _shiftArraySize;

        public WorkDiagram(int numberOfVarancy)
        {
            _shiftArraySize = numberOfVarancy * 2;
            _shift = new char[_shiftArraySize];  //even = daily shift || odd = nightly shift
        }

        public char[] Shift
        {
            get => _shift; 
            set => _shift = value; 
        }

        public Dictionary<byte, char[]> Diagram
        {
            get =>  _diagram; 
            set => _diagram = value; 
        }
    }
}
