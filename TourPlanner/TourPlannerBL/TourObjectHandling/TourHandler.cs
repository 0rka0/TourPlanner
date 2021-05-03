using TourPlannerBL.Mapquest;
using TourPlannerDAL.Files;
using TourPlannerDAL.Databases;
using TourPlannerModels;
using log4net;
using System.Reflection;
using System;
using TourPlannerBL.StringPrep;
using TourPlannerModels.TourObject;

namespace TourPlannerBL.TourObjectHandling
{
    static public class TourHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static public void AddTour(string start, string goal, string desc, string inf)
        {
            _logger.Info("Attempting to add Tour");

            try
            {
                TourInformationResponseObject information = MapQuestHandler.GetTourInformation(start, goal);
                Tour tour = CreateTourObject(information, StringPreparer.BuildName(start, goal), desc, inf);

                InsertTour(tour);

                MapQuestHandler.GetImage(information, tour.Image);
                _logger.Info("Add success");
            }
            catch (Exception e)
            {
                _logger.Error("Adding process led to following error: " + e.Message);
            }
        }

        static public void DeleteTour(Tour tour)
        {
            _logger.Info("Attempting to delete Tour");

            try
            {
                IDatabase db = TourDatabaseHandler.GetInstance();
                db.DeleteEntry(tour.Id);
                FileHandler.DeleteImage(Configuration.ImagePath + tour.Image);

                _logger.Info("Deletion success");
            }
            catch (Exception e)
            {
                _logger.Error("Deletion process led to following error: " + e.Message);
            }
        }

        static public void EditTour(string name, string description, string information, Tour tour)
        {
            _logger.Info("Attempting to edit Tour");

            try
            {
                tour.SetEditData(name, description, information);

                IDatabase db = TourDatabaseHandler.GetInstance();
                db.UpdateEntry(tour);

                _logger.Info("Editing success");
            }
            catch (Exception e)
            {
                _logger.Error("Editing process led to following error: " + e.Message);
            }
        }

        static public void CopyTour(Tour tour)
        {
            _logger.Info("Attempting to copy Tour");

            try
            {
                Tour copy = tour.Clone();

                InsertTour(copy);

                FileHandler.CopyImage(tour.Image, copy.Image);

                _logger.Info("Copying success");
            }
            catch (Exception e)
            {
                _logger.Error("Copying process led to following error: " + e.Message);
            }
        }

        static Tour CreateTourObject(TourInformationResponseObject information, string name, string desc, string inf)
        {
            return new Tour(name, desc, inf, information.route.distance.ToString());
        }

        static void InsertTour(Tour tour)
        {
            IDatabase db = TourDatabaseHandler.GetInstance();
            tour.Id = db.GetMaxId();
            tour.Image = StringPreparer.BuildFilename(tour.Id, tour.Name);
            db.InsertEntry(tour);
        }
    }
}
