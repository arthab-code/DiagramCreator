using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public interface ISaveWorkPlace
    {
        void SaveWorkPlaceToDatabase(string workPlaceName);
    }
}
