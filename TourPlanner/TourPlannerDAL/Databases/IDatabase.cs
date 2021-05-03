using System.Collections.Generic;
using TourPlannerModels.TourObject;

namespace TourPlannerDAL.Databases
{
    public interface IDatabase
    {
        public IEnumerable<ITourObject> SelectEntries(int id = 0);
        public void UpdateEntry(ITourObject tourObject);
        public void InsertEntry(ITourObject tourObject);
        public void DeleteEntry(int id);
        public int GetMaxId();
    }
}
