using System;
using System.Collections.Generic;

namespace TourPlannerModels
{
    public class Tour : ITourPrototype
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

        Tour(Tour other)
        {
            this.Id = other.Id;
            this.Name = other.Name;
            this.TourDescription = other.TourDescription;
            this.RouteInformation = other.RouteInformation;
            this.Distance = other.Distance;
            this.Image = other.Image;
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

        public void SetEditData(string name, string desc, string inf)
        {
            if (!String.IsNullOrEmpty(name))
                Name = name;
            if (!String.IsNullOrEmpty(desc))
                TourDescription = desc;
            if (!String.IsNullOrEmpty(inf))
                RouteInformation = inf;
        }

        public void AddLog(TourLog log)
        {
            LogList.Add(log);
        }

        public Tour Clone()
        {
            return new Tour(this);
        }
    }
}
