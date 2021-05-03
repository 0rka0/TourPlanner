using log4net;
using System;
using System.Collections.Generic;
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
            _logger.Info("Attempting to add new TourLog");
            try
            { 
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

        static public void EditTourLogs(IEnumerable<TourLog> tourLogs)
        {
            try
            {
                IDatabase db = TourLogDatabaseHandler.GetInstance();
                foreach (TourLog log in tourLogs)
                {
                    db.UpdateEntry(log);
                }

                _logger.Info("Edit success");
            }
            catch (Exception e)
            {
                _logger.Error("Updating process led to following error: " + e.Message);
            }
        }

        static public void DelTourLog(int id)
        {
            _logger.Info("Attempting to delete tourlog");
            try
            {
                IDatabase db = TourLogDatabaseHandler.GetInstance();
                db.DeleteEntry(id);

                _logger.Info("Deletion success");
            }
            catch (Exception e)
            {
                _logger.Error("Deletion process led to following error: " + e.Message);
            }
        }
    }
}
