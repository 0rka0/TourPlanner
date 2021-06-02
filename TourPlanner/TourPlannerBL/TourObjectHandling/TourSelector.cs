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
            _logger.Info("Attempting to select Tour entries from database");

            try
            {
                List<Tour> tourList = (List<Tour>)_db.SelectEntries();

                tourList = TourLogSelector.FillToursWithLogs(tourList);
                tourList = AttractionSelector.FillToursWithAttractions(tourList);

                _logger.Info("Selecting success");
                return tourList;
            }
            catch (Exception e)
            {
                _logger.Error("Selecting process led to following error: " + e.Message);
                return new List<Tour>();
            }
        }

        static public IEnumerable<Tour> Search(string filter)
        {
            _logger.Info("Attempting to filter Tours that contain " + filter);
            IEnumerable<Tour> tours = GetTours();
            tours = TourLogSelector.FillToursWithLogs((List<Tour>)tours);

            return tours.Where(x => (
                x.Name.ToLower().Contains(filter.ToLower()) ||
                x.TourDescription.ToLower().Contains(filter.ToLower()) ||
                x.RouteInformation.ToLower().Contains(filter.ToLower()) ||
                    ((IEnumerable<TourLog>)x.LogList).Where(y => (
                        y.Date.Year.ToString().ToLower().Equals(filter.ToLower()) ||
                        String.Format("{0}/{1}", y.Date.Year, y.Date.Month).ToLower().Equals(filter.ToLower()) ||
                        String.Format("{1}/{0}", y.Date.Year, y.Date.Month).ToLower().Equals(filter.ToLower()) ||
                        String.Format("{0}/{1}/{2}", y.Date.Year, y.Date.Month, y.Date.Day).ToLower().Equals(filter.ToLower()) ||
                        String.Format("{2}/{1}/{0}", y.Date.Year, y.Date.Month, y.Date.Day).ToLower().Equals(filter.ToLower()) ||
                        String.Format("{0}.{1}", y.Date.Year, y.Date.Month).ToLower().Equals(filter.ToLower()) ||
                        String.Format("{1}.{0}", y.Date.Year, y.Date.Month).ToLower().Equals(filter.ToLower()) ||
                        String.Format("{0}.{1}.{2}", y.Date.Year, y.Date.Month, y.Date.Day).ToLower().Equals(filter.ToLower()) ||
                        String.Format("{2}.{1}.{0}", y.Date.Year, y.Date.Month, y.Date.Day).ToLower().Equals(filter.ToLower()) ||
                        y.Report.ToLower().Contains(filter.ToLower()) ||
                        y.Rating.ToString().ToLower().Contains(filter.ToLower()) ||
                        ((int)y.Rating).ToString().Contains(filter.ToLower()) ||
                        y.Weather.ToString().ToLower().Contains(filter.ToLower()) ||
                        y.Traffic.ToString().ToLower().Contains(filter.ToLower())
                    )).Any() ||
                    ((IEnumerable<Attraction>)x.AttList).Where(z => (
                        z.Name.ToLower().Contains(filter.ToLower()) ||
                        z.Address.ToLower().Contains(filter.ToLower())
                    )).Any()
                ));
        }
    }
}
