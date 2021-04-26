using System.Collections.Generic;
using System.Diagnostics;
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

            DatabaseHandler db = DatabaseHandler.GetInstance();
            tour = db.InsertTourEntry(tour);

            tour.Image = GetImage(information, StringPreparer.BuildFilename(tour.Id, tour.Name));
            db.InsertImage(tour.Image, tour.Id);
        }

        static public void DeleteTour(int id)
        {
            DatabaseHandler db = DatabaseHandler.GetInstance();
            db.DeleteTourEntry(id);
        }

        static public void CopyTour(int id)
        {
            DatabaseHandler db = DatabaseHandler.GetInstance();
        }

        static Tour CreateTourObject(TourInformationResponse information, string name, string desc, string inf)
        {
            return new Tour(name, desc, inf, information.route.distance.ToString());
        }

        static string GetImage(TourInformationResponse information, string filename)
        {
            return MapQuestHandler.GetImage(information, filename);
        }

        static public IEnumerable<Tour> GetTours()
        {
            DatabaseHandler db = DatabaseHandler.GetInstance();
            return db.SelectAllTourEntries();
        }
    }
}
