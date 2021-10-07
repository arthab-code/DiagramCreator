using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public class WorkPlace
    {
        private string _placeName;
        private string _cityOfPlace;

        public string PlaceName
        {
            get => _placeName; 
            set => _placeName = value; 
        }

        public string CityOfPlace
        {
            get => _cityOfPlace; 
            set => _cityOfPlace = value; 
        }
    }
}
