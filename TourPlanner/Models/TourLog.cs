using System;

namespace TourPlannerModels
{
    public class TourLog : ITourContent
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Report { get; set; }
        public int Distance { get; set; }
        public int TotalTime { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int AvgSpeed { get; set; }
        public int TourId { get; set; }

        public TourLog(int id, DateTime date, string report, int dis, int totalTime, int rating, string comment, int avgSpeed, int tourId)
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
            TourId = tourId;
        }
    }
}
