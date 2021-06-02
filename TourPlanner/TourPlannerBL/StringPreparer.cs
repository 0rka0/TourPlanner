using System;
using TourPlannerModels;

namespace TourPlannerBL.StringPrep
{
    static public class StringPreparer
    {
        static public string BuildRequest(string start, string goal)
        {
            return String.Format("{0}?key={1}&from={2}&to={3}", Configuration.UrlDirectionsApi, Configuration.Key, start, goal);
        }

        static public string BuildRequest(string requestString)
        {
            return String.Format("{0}?key={1}&{2}", Configuration.UrlStaticMapApi, Configuration.Key, requestString);
        }

        static public string BuildGoogleRequest(string goal)
        {
            return String.Format("{0}query={1}+point+of+interest&key={2}", Configuration.UrlGooglePlacesApi, goal, Configuration.GoogleKey);
        }

        static public string BuildName(string start, string goal)
        {
            return String.Format("{0}-{1}", start, goal);
        }

        static public string BuildFilename(int id, string name)
        {
            return String.Format("{0}{1}.png", id, name);
        }

        static public string BuildReportName(DateTime name)
        {
            return $"Report_{name.ToString("yyyy_MM_dd_HH_mm_ss")}.pdf";
        }

        static public string BuildSummaryName(DateTime name)
        {
            return $"Summary_{name.ToString("yyyy_MM_dd_HH_mm_ss")}.pdf";
        }

        static public Tuple<string, string> ExtractLocationFromFilename(string filename)
        {
            string start = filename.Substring(1, filename.IndexOf("-") - 1);
            string goal = filename.Substring(filename.IndexOf("-") + 1, filename.IndexOf(".") - filename.IndexOf("-") - 1);

            return Tuple.Create(start, goal);
        }
    }
}
