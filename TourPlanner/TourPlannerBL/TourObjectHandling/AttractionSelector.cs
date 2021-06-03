using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using TourPlannerDAL.Databases;
using TourPlannerModels.TourObject;

namespace TourPlannerBL.TourObjectHandling
{
    public class AttractionSelector
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static IDatabase _db;

        static public void Init(IDatabase db)
        {
            _db = db;
        }

        public static List<Tour> FillToursWithAttractions(List<Tour> tourList)
        {
            List<Attraction> attList = new List<Attraction>();

            _logger.Info("Attempting to add corresponding Attractions to Tours");
            try
            {
                foreach (Tour tour in tourList)
                {
                    attList.Clear();
                    attList = (List<Attraction>)SelectAttractionsById(tour.Id);

                    tour.AttList.AddRange(attList);
                }
                _logger.Info("Attractions successfully added");
            }
            catch (Exception e)
            {
                _logger.Info("Adding process led to following error: " + e.Message);
            }

            return tourList;
        }

        public static IEnumerable<ITourObject> SelectAttractionsById(int id)
        {
            return _db.SelectEntries(id);
        }
    }
}
