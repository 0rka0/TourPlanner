using System.Collections.Generic;
using System.Linq;
using TourPlannerDAL.Databases;
using TourPlannerModels.TourObject;
using log4net;
using System.Reflection;
using System;

namespace TourPlannerBL.TourObjectHandling
{
    static public class TourSelector
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        private static IDatabase _db;

        static public void Init(IDatabase db)
        {
            _db = db;
        }

        static public IEnumerable<Tour> GetTours()
        {
            _logger.Info("Attempting to get Tour entries from database");

            try
            {
                List<Tour> tourList = (List<Tour>)_db.SelectEntries();

                tourList = FillToursWithLogs(tourList);

                return tourList;
            }
            catch (Exception e)
            {
                _logger.Error("Selecting process led to following error: " + e.Message);
                return new List<Tour>();
            }
        }

        private static List<Tour> FillToursWithLogs(List<Tour> tourList)
        {
            List<TourLog> logList = new List<TourLog>();

            foreach (Tour tour in tourList)
            {
                logList.Clear();
                logList = (List<TourLog>)TourLogSelector.SelectTourLogsById(tour.Id);

                tour.LogList.AddRange(logList);
            }

            return tourList;
        }

        static public IEnumerable<Tour> Search(string filter)
        {
            IEnumerable<Tour> tours = GetTours();

            return tours.Where(x => x.Name.ToLower().Contains(filter.ToLower()));
        }
    }
}
