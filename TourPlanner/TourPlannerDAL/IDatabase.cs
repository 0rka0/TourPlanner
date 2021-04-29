using System.Collections.Generic;
using TourPlannerModels;

namespace TourPlannerDAL
{
    public interface IDatabase
    {
        public IEnumerable<ITourContent> SelectEntries(int id = 0);
        public void UpdateEntry(ITourContent tourObject);
        public void InsertEntry(ITourContent tourObject);
        public void DeleteEntry(int id);
        public int GetMaxId();
    }
}
