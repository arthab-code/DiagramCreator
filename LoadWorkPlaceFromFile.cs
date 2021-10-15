using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Grafik
{
    public class LoadWorkPlaceFromFile : ILoadWorkPlace
    {
        private string _pathDirectories = "AppData/WorkPlaces";
        public List<string> LoadAllPlaces()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_pathDirectories);

            var directoriesTemp = directoryInfo.GetDirectories();

            List<string> directories = new List<string>();

            foreach (var directory in directoriesTemp)
            {
                directories.Add(directory.Name);
            }

            return directories;
        }
    }
}
