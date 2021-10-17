using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Grafik
{
    public class WorkPlacesFileDatabase : IWorkPlacesDatabase
    {
        private Paths _paths = new Paths();
        public void CreateWorkPlace(string workPlaceName)
        {    
                DirectoryInfo directoryInfo = new DirectoryInfo(_paths._workerPlacePath);
                directoryInfo.CreateSubdirectory(workPlaceName);

                Console.WriteLine("Stworzono miejsce pracy : " + workPlaceName);
        }

        public void DeleteWorkPlace(string name)
        {
            throw new NotImplementedException();
        }

        public List<string> ReadWorkPlaces()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_paths._workerPlacePath);

            var directoriesTemp = directoryInfo.GetDirectories();

            List<string> directories = new List<string>();

            foreach (var directory in directoriesTemp)
            {
                directories.Add(directory.Name);
            }

            return directories;
        }

        public void UpdateWorkPlace(string name)
        {
            throw new NotImplementedException();
        }
    }
}
