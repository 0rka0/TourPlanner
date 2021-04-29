using log4net;
using System;
using System.Reflection;
using TourPlannerDAL;
using TourPlannerModels;

namespace TourPlannerBL
{
    static public class TourLogHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static public void AddTourLog(int tourId)
        {
            try
            {
                _logger.Info("AddTourLog successfully called");
                IDatabase db = TourLogDatabaseHandler.GetInstance();
                int id = db.GetMaxId();
                ITourContent tourLog = new TourLog(id, tourId);
                db.InsertEntry(tourLog);

                _logger.Info("Add success");
            }
            catch (Exception e)
            {
                _logger.Error("Adding process led to following error: " + e.Message);
            }
        }
    }
}
