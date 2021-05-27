using System.Collections.Generic;
using TourPlannerDAL.Databases;
using TourPlannerModels.TourObject;

namespace TourPlannerBL.TourObjectHandling
{
    public static class TourLogSelector
    {
        private static IDatabase _db;

        static public void Init(IDatabase db)
        {
            _db = db;
        }

        public static IEnumerable<ITourObject> SelectTourLogsById(int id)
        {
            return _db.SelectEntries(id);
        }
    }
}
