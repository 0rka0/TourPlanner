using System;
using System.Collections.Generic;
using System.Text;
using TourPlannerDAL;

namespace TourPlannerBL
{
    //used to add new tours to the system
    static public class TourHandler
    {
        static public Tour AddTour(string start, string goal)
        {
            TourInformationResponse information = MapQuestHandler.GetTourInformation(start, goal);
            Tour tour = CreateTour(information, StringPreparer.BuildName(start, goal));

            DatabaseHandler db = DatabaseHandler.GetInstance();
            db.InsertTour(tour);

            tour.Location = GetImage(information, StringPreparer.BuildFilename(tour.Id, tour.Name));
            db.InsertImage(tour.Location, tour.Id);

            return tour;
        }

        static Tour CreateTour(TourInformationResponse information, string name)
        {
            Tour tour = new Tour(name, "first tour", "this is a tour", information.route.distance.ToString());
            return tour;
        }

        static string GetImage(TourInformationResponse information, string filename)
        {
            string path = MapQuestHandler.GetImage(information, filename);
            return path;
        }
    }
}
