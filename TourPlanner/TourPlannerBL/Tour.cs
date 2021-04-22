﻿using System;
using System.Collections.Generic;

namespace TourPlannerBL
{
    public class Tour
    {
        public int id { get; private set; }
        public string Name { get; private set; }
        public string TourDescription { get; private set; }
        public string RouteInformation { get; private set; }
        public string Distance { get; set; }
        public string Location { get; private set; }

        List<TourLog> LogList = new List<TourLog>();

        public Tour(string name, string desc, string inf, string dist, string loc)
        {
            Name = name;
            TourDescription = desc;
            RouteInformation = inf;
            Distance = dist;
            Location = loc;
        }

        public void AddLog(TourLog log)
        {
            LogList.Add(log);
        }
    }
}
