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
            Tour tour = CreateTour(start, goal);
            DatabaseHandler db = DatabaseHandler.GetInstance();
            db.InsertTour(tour);

            return tour;
        }

        static Tour CreateTour(string start, string goal)
        {
            TourInformationResponse information = MapQuestHandler.GetTourInformation(start, goal);
            string path = MapQuestHandler.GetImage(information);

            Tour tour = new Tour(StringPreparer.NameBuilder(start, goal), "first tour", "this is a tour", information.route.distance.ToString(), path);

            return tour;
        }
    }
}
