using System.Collections.Generic;
using System.Linq;
using TourPlannerDAL;
using TourPlannerModels;

namespace TourPlannerBL
{
    static public class TourSelector
    {
        static public IEnumerable<Tour> GetAllTours()
        {
            DatabaseHandler db = DatabaseHandler.GetInstance();
            return db.SelectAllTourEntries();
        }

        static public IEnumerable<Tour> Search(string filter)
        {
            IEnumerable<Tour> tours = GetAllTours();

            return tours.Where(x => x.Name.ToLower().Contains(filter.ToLower()));
        }
    }
}
