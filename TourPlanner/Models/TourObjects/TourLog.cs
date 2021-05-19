using System;
using TourPlannerModels.Types;

namespace TourPlannerModels.TourObject
{
    public class TourLog : ITourObject
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Distance { get; set; }
        public string Report { get; set; }
        public float TotalTime { get; set; }
        public Ratings Rating { get; set; }
        public int AvgSpeed { get; set; }
        public WeatherTypes Weather { get; set; }
        public TrafficTypes Traffic { get; set; }
        public int Breaks { get; set; }
        public int GroupSize { get; set; }
        public int TourId { get; set; }

        public TourLog()
        { }

        public TourLog(int id, DateTime date, float dis, string report, float totalTime, int rating, int avgSpeed, int weather, int traffic, int breaks, int groupSize, int tourId)
        {
            Id = id;
            Date = date;
            Distance = dis;
            Report = report;
            TotalTime = totalTime;
            Rating = TypeResolver.GetRating(rating);
            AvgSpeed = avgSpeed;
            Weather = TypeResolver.GetWeatherType(weather);
            Traffic = TypeResolver.GetTrafficType(traffic);
            Breaks = breaks;
            GroupSize = groupSize;
            TourId = tourId;
        }

        public TourLog(int id, int tourId)
        {
            Id = id;
            Date = DateTime.Now;
            Distance = 0.0f;
            Report = "";
            TotalTime = 0.0f;
            Rating = 0;
            AvgSpeed = 0;
            Weather = WeatherTypes.NoData;
            Traffic = TrafficTypes.NoData;
            Breaks = 0;
            GroupSize = 0;
            TourId = tourId;
        }
    }
}
