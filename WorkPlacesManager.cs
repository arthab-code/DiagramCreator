using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafik
{
    public class WorkPlacesManager
    {
        private IWorkPlacesDatabase _workFilesDatabase = new WorkPlacesFileDatabase();
        private List<string> _workPlaces = new List<string>();
        private Paths _paths = new Paths();

        public WorkPlacesManager()
        {
            _workPlaces = new List<string>();
        }

        public List<string> WorkPlaces
        {
            get => _workPlaces;
            set => _workPlaces = value;
        }

        public void LoadWorkPlacesToList()
        {
            /* Clear list*/
            _workPlaces.Clear();

            _workPlaces = _workFilesDatabase.ReadWorkPlaces();
        }

        public void DeleteWorkPlace(string name)
        {
            _workFilesDatabase.DeleteWorkPlace(name);

            LoadWorkPlacesToList();
        }

        public void CreatePlace(string name)
        {
            _workFilesDatabase.CreateWorkPlace(name);
            LoadWorkPlacesToList();
        }

        public void UpdatePlace(string oldName, string newName)
        {
            _workFilesDatabase.UpdateWorkPlace(oldName, newName);
            LoadWorkPlacesToList();
        }
    }
}
