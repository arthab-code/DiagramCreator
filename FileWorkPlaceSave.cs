using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Grafik
{
    class FileWorkPlaceSave : ISaveWorkPlace
    {
        private string _pathDirectories = "AppData/WorkPlaces";

        public void SaveWorkPlaceToDatabase(string workPlaceName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_pathDirectories);
            directoryInfo.CreateSubdirectory(workPlaceName);

            Console.WriteLine("Stworzono miejsce pracy : "+workPlaceName);
        }

    }
}
