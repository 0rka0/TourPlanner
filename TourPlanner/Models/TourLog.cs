using System;

namespace TourPlannerModels
{
    public class TourLog : ITourContent
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Report { get; set; }
        public string Distance { get; set; }
        public string TotalTime { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string AvgSpeed { get; set; }
        public int TourId { get; set; }

        public TourLog(int id, DateTime date, string report, string dis, string totalTime, int rating, string comment, string avgSpeed, int tourId)
        {
            Id = id;
            Date = date;
            Report = report;
            Distance = dis;
            TotalTime = totalTime;
            Rating = rating;
            Comment = comment;
            AvgSpeed = avgSpeed;
            TourId = tourId;
        }

        public TourLog(int id, int tourId)
        {
            Id = id;
            Date = DateTime.Now;
            Report = "";
            Distance = "";
            TotalTime = "";
            Rating = 0;
            Comment = "";
            AvgSpeed = "";
            TourId = tourId;
        }
    }
}
