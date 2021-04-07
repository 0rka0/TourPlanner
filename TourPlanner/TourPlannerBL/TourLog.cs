using System;

namespace TourPlannerBL
{
    public class TourLog
    {
        public DateTime Date { get; private set; }
        public string Report { get; private set; }
        public int Distance { get; private set; }
        public int TotalTime { get; private set; }
        public int Rating { get; private set; }
        public string Comment { get; private set; }
        public int AvgSpeed { get; private set; }
    }
}
