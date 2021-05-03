using System;

namespace TourPlannerModels.TourObject
{
    public class TourLog : ITourObject
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Duration { get; set; }
        public string Distance { get; set; }
        public string Report { get; set; }
        public string TotalTime { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string AvgSpeed { get; set; }
        public int TourId { get; set; }

        public TourLog(int id, DateTime date, string duration, string dis, string report, string totalTime, int rating, string comment, string avgSpeed, int tourId)
        {
            Id = id;
            Date = date;
            Duration = duration;
            Distance = dis;
            Report = report;
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
            Duration = "";
            Distance = "";
            Report = "";
            TotalTime = "";
            Rating = 0;
            Comment = "";
            AvgSpeed = "";
            TourId = tourId;
        }
    }
}
