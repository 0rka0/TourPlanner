using System.Collections.Generic;
using TourPlannerDAL;
using TourPlannerModels;

namespace TourPlannerBL
{
    //used to add new tours to the system
    //overlap with TourFactory - to be reconsidered
    static public class TourHandler
    {
        static public void AddTour(string start, string goal, string desc, string inf)
        {
            TourInformationResponse information = MapQuestHandler.GetTourInformation(start, goal);
            Tour tour = CreateTourObject(information, StringPreparer.BuildName(start, goal), desc, inf);

            InsertTour(tour);

            MapQuestHandler.GetImage(information, tour.Image);
        }

        static public void DeleteTour(Tour tour)
        {
            DatabaseHandler db = DatabaseHandler.GetInstance();
            db.DeleteTourEntry(tour.Id);
            FileHandler.DeleteImage(Configuration.ImagePath + tour.Image);
        }

        static public void EditTour(string name, string description, string information, Tour tour)
        {
            tour.SetEditData(name, description, information);

            DatabaseHandler db = DatabaseHandler.GetInstance();
            db.UpdateTourEntry(tour);
        }

        static public void CopyTour(Tour tour)
        {
            Tour copy = tour.Clone();

            InsertTour(copy);

            FileHandler.CopyImage(tour.Image, copy.Image);
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
