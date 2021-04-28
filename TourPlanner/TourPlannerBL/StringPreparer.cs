using System;

namespace TourPlannerBL
{
    static public class StringPreparer
    {
        static readonly string _urlDirections = "http://www.mapquestapi.com/directions/v2/route";
        static readonly string _urlStaticMap = "http://www.mapquestapi.com/staticmap/v5/map";
        static readonly string _key = "A1H6TsijwzAZ3cp7vu5cGAmVqEysE6gy"; //to be transfered into config file

        static public string BuildRequest(string start, string goal)
        {
            return String.Format("{0}?key={1}&from={2}&to={3}", _urlDirections, _key, start, goal);
        }

        static public string BuildRequest(string requestString)
        {
            return String.Format("{0}?key={1}&{2}", _urlStaticMap, _key, requestString);
        }

        static public string BuildName(string start, string goal)
        {
            return String.Format("{0}-{1}", start, goal);
        }

        static public string BuildFilename(int id, string name)
        {
            return String.Format("{0}{1}.png", id, name);
        }

        static public string BuildPdfName(DateTime name)
        {
            return $"Report_{name.ToString("yyyy_MM_dd_HH_mm_ss")}";
        }
    }
}
