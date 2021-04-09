using System;
using System.Collections.Generic;
using System.Text;

namespace TourPlannerBL
{
    //used to add new tours to the system
    static public class TourHandler
    {
        static public Tour CreateTour(string start, string goal)
        {
            MapQuestHandler.GetTourInformation(start, goal);

            return new Tour(start, "first tour", "this is a tour", "20 km");
        }
    }
}
