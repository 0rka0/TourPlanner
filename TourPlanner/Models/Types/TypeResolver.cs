using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerModels.Types
{
    static public class TypeResolver
    {
        static public WeatherTypes GetWeatherType(int weather)
        {
            switch (weather)
            {
                case 0:
                    return WeatherTypes.Sunny;
                case 1:
                    return WeatherTypes.Rainy;
                case 2:
                    return WeatherTypes.Windy;
                case 3:
                    return WeatherTypes.Snowy;
                case 4:
                    return WeatherTypes.NoData;
                default:
                    return WeatherTypes.NoData;
            }
        }

        static public TrafficTypes GetTrafficType(int traffic)
        {
            switch (traffic)
            {
                case 0:
                    return TrafficTypes.TrafficJam;
                case 1:
                    return TrafficTypes.Heavy;
                case 2:
                    return TrafficTypes.Medium;
                case 3:
                    return TrafficTypes.Light;
                case 4:
                    return TrafficTypes.Free;
                case 5:
                    return TrafficTypes.NoData;
                default:
                    return TrafficTypes.NoData;
            }
        }

        static public Ratings GetRating(int rating)
        {
            switch (rating)
            {
                case 10:
                    return Ratings.Excellent;
                case 9:
                    return Ratings.Amazing;
                case 8:
                    return Ratings.Great;
                case 7:
                    return Ratings.Good;
                case 6:
                    return Ratings.AboveAverage;
                case 5:
                    return Ratings.Average;
                case 4:
                    return Ratings.BelowAverage;
                case 3:
                    return Ratings.Bad;
                case 2:
                    return Ratings.Lousy;
                case 1:
                    return Ratings.Awful;
                case 0:
                    return Ratings.NoData;
                default:
                    return Ratings.NoData;
            }
        }
    }
}
