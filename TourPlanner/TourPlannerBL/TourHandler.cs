using TourPlannerDAL;
using TourPlannerModels;
using log4net;
using System.Reflection;
using System;

namespace TourPlannerBL
{
    //used to add new tours to the system
    //overlap with TourFactory - to be reconsidered
    static public class TourHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static public void AddTour(string start, string goal, string desc, string inf)
        {
            _logger.Info("Attempting to add Tour");

            try
            {
                TourInformationResponse information = MapQuestHandler.GetTourInformation(start, goal);
                Tour tour = CreateTourObject(information, StringPreparer.BuildName(start, goal), desc, inf);

                InsertTour(tour);

                MapQuestHandler.GetImage(information, tour.Image);
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
                DatabaseHandler db = DatabaseHandler.GetInstance();
                db.DeleteTourEntry(tour.Id);
                FileHandler.DeleteImage(Configuration.ImagePath + tour.Image);
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

                DatabaseHandler db = DatabaseHandler.GetInstance();
                db.UpdateTourEntry(tour);
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
            }
            catch (Exception e)
            {
                _logger.Error("Editing process led to following error: " + e.Message);
            }
        }

        static Tour CreateTourObject(TourInformationResponse information, string name, string desc, string inf)
        {
            return new Tour(name, desc, inf, information.route.distance.ToString());
        }

        static void InsertTour(Tour tour)
        {
            DatabaseHandler db = DatabaseHandler.GetInstance();
            tour.Id = db.GetMaxId();
            tour.Image = StringPreparer.BuildFilename(tour.Id, tour.Name);
            db.InsertTourEntry(tour);
        }
    }
}
