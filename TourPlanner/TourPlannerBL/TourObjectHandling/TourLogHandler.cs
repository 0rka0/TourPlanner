using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using TourPlannerDAL.Databases;
using TourPlannerModels.TourObject;

namespace TourPlannerBL.TourObjectHandling
{
    static public class TourLogHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static IDatabase _db;

        static public void Init(IDatabase db)
        {
            _db = db;
        }

        static public void AddNewTourLog(int tourId)
        {
            _logger.Info("Attempting to add new TourLog");
            try
            { 
                int id = _db.GetMaxId();
                ITourObject tourLog = new TourLog(id, tourId);
                _db.InsertEntry(tourLog);

                _logger.Info("Add success");
            }
            catch (Exception e)
            {
                _logger.Error("Adding process led to following error: " + e.Message);
            }
        }

        static public void AddImportedTourLog(TourLog log)
        {
            _logger.Info("Attempting to add existing TourLog");
            try
            {
                _db.InsertEntry(log);

                _logger.Info("Add success");
            }
            catch (Exception e)
            {
                _logger.Error("Adding process led to following error: " + e.Message);
            }
        }

        static public void EditTourLogs(IEnumerable<TourLog> tourLogs)
        {
            _logger.Info("Attempting to edit TourLogs");
            try
            {
                foreach (TourLog log in tourLogs)
                {
                    _db.UpdateEntry(log);
                }

                _logger.Info("Edit success");
            }
            catch (Exception e)
            {
                _logger.Error("Editing process led to following error: " + e.Message);
            }
        }

        static public void DelTourLog(int id)
        {
            _logger.Info("Attempting to delete tourlog");
            try
            {
                _db.DeleteEntry(id);

                _logger.Info("Deletion success");
            }
            catch (Exception e)
            {
                _logger.Error("Deletion process led to following error: " + e.Message);
            }
        }

        static public void ClearData()
        {
            _db.ClearDb();
        }
    }
}
