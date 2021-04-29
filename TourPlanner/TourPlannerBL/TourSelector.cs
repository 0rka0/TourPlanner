using System.Collections.Generic;
using System.Linq;
using TourPlannerDAL;
using TourPlannerModels;
using log4net;
using System.Reflection;
using System;

namespace TourPlannerBL
{
    static public class TourSelector
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static public IEnumerable<Tour> GetTours()
        {
            _logger.Info("Attempting to get Tour entries from database");

            try
            {
                IDatabase db = TourDatabaseHandler.GetInstance();
                return (IEnumerable<Tour>)db.SelectEntries();
            }
            catch (Exception e)
            {
                _logger.Error("Selecting process led to following error: " + e.Message);
                return new List<Tour>();
            }
        }

        static public IEnumerable<Tour> Search(string filter)
        {
            IEnumerable<Tour> tours = GetTours();

            return tours.Where(x => x.Name.ToLower().Contains(filter.ToLower()));
        }
    }
}
