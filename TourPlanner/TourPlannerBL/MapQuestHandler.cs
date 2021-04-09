using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TourPlannerBL
{
    //used to interact with the MapQuest API
    static public class MapQuestHandler
    {
        static readonly HttpClient client = new HttpClient();
        static readonly string _urlDirections = "http://www.mapquestapi.com/directions/v2/route";
        static readonly string _urlStaticMap = "http://www.mapquestapi.com/staticmap/v5/map";
        static readonly string _key = "A1H6TsijwzAZ3cp7vu5cGAmVqEysE6gy"; //to be transfered into config file

        static public void GetTourInformation(string start, string goal)
        {
            string request = BuildDirectionsRequest(start, goal);
            string directionsString;
            Task<string> task = Task.Run<string>(async () => await GetRoute(request));
            directionsString = task.Result; //response from MapQuest API

            Debug.WriteLine(directionsString);
        }

        static string BuildDirectionsRequest(string start, string goal)
        {
            return String.Format("{0}?key={1}&from={2}&to={3}", _urlDirections, _key, start, goal);
        }

        static string BuildStaticMapRequest(string session, string boundingBox)
        {
            return "";
        }

        static async Task<string> GetRoute(string request)
        {
            HttpResponseMessage response = await client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(responseBody);

            return responseBody;
        }
    }
}
