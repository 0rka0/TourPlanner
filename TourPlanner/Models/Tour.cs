using System.Collections.Generic;

namespace TourPlannerModels
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public string TourDescription { get; private set; }
        public string RouteInformation { get; private set; }
        public string Distance { get; set; }
        public string Image { get; set; }

        List<TourLog> LogList = new List<TourLog>();

        public Tour(string name, string desc, string inf, string dist)
        {
            TourDescription = "";
            RouteInformation = "";
            Name = name;
            TourDescription += desc;
            RouteInformation += inf;
            Distance = dist;
        }

        public Tour(int id, string name, string desc, string inf, string dist, string img)
        {
            Id = id;
            Name = name;
            TourDescription = desc;
            RouteInformation = inf;
            Distance = dist;
            Image = img;
        }

        public void AddLog(TourLog log)
        {
            LogList.Add(log);
        }
    }
}
