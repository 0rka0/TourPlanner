﻿using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using TourPlannerDAL.Databases;
using TourPlannerModels.TourObject;

namespace TourPlannerBL.TourObjectHandling
{
    public static class TourLogSelector
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static IDatabase _db;

        static public void Init(IDatabase db)
        {
            _db = db;
        }

        public static List<Tour> FillToursWithLogs(List<Tour> tourList)
        {
            List<TourLog> logList = new List<TourLog>();

            _logger.Info("Attempting to add corresponding TourLogs to Tours");
            try
            {
                foreach (Tour tour in tourList)
                {
                    logList.Clear();
                    logList = (List<TourLog>)SelectTourLogsById(tour.Id);

                    tour.LogList.AddRange(logList);
                }
                _logger.Info("Logs successfully added");
            }
            catch (Exception e)
            {
                _logger.Info("Adding process led to following error: " + e.Message);
            }

            return tourList;
        }

        public static IEnumerable<ITourObject> SelectTourLogsById(int id)
        {
            return _db.SelectEntries(id);
        }
    }
}
