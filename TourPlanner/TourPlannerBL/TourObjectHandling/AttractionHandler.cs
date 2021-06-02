using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using TourPlannerDAL.Databases;
using TourPlannerModels.TourObject;

namespace TourPlannerBL.TourObjectHandling
{
    class AttractionHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static IDatabase _db;

        static public void Init(IDatabase db)
        {
            _db = db;
        }

        static public void NewAddAttractions(AttractionResponseObject attractions, int tid)
        {
            _logger.Info("Attempting to add new Attractions");
            try
            {
                InsertAttractions(GetAttractionList(attractions, tid), true);
            }
            catch (Exception e)
            {

            }
        }

        static public void AddImportedAttractions(List<Attraction> attractions)
        {
            _logger.Info("Attempting to add imported Attraction");
            try
            {
                InsertAttractions(attractions, false);

                _logger.Info("Add success");
            }
            catch (Exception e)
            {
                _logger.Error("Adding process led to following error: " + e.Message);
            }
        }

        static private List<Attraction> GetAttractionList(AttractionResponseObject attractions, int tid)
        {
            List<Attraction> attractionList = new List<Attraction>();
            foreach (Result attraction in attractions.results)
            {
                attractionList.Add(new Attraction(attraction.place_id, attraction.name, (float)attraction.rating, attraction.user_ratings_total, attraction.formatted_address, tid));
            }
            return attractionList;
        }

        static private void InsertAttractions(List<Attraction> attractionList, bool newAttractions)
        {
            foreach(Attraction attraction in attractionList)
            {
                if(newAttractions)
                    attraction.Id = _db.GetMaxId();
                _db.InsertEntry(attraction);
            }
        }
    }
}
