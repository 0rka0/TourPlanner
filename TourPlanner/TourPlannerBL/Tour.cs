﻿using System;
using System.Collections.Generic;

namespace TourPlannerBL
{
    public class Tour
    {
        public string Name { get; private set; }
        public string TourDescription { get; private set; }
        public string RouteInformation { get; private set; }
        public string Distance { get; set; }

        List<TourLog> LogList = new List<TourLog>();

        public Tour(string name, string desc, string inf, string dist)
        {
            Name = name;
            TourDescription = desc;
            RouteInformation = inf;
            Distance = dist;
        }

        public void AddLog(TourLog log)
        {
            LogList.Add(log);
        }
    }
}
