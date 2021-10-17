using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public interface IWorkPlacesDatabase
    {
        void CreateWorkPlace(string name);
        List<string> ReadWorkPlaces();
        void UpdateWorkPlace(string name);
        void DeleteWorkPlace(string name);
    }
}
