using log4net;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using TourPlannerBL.StringPrep;

namespace TourPlannerBL.API.GooglePlaces
{
    static public class GooglePlacesHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static readonly HttpClient client = new HttpClient();

        static public AttractionResponseObject RequestAttractions(string goal)
        {
            _logger.Info("Requesting Attractions from Google Places API");

            try
            {
                AttractionResponseObject response = GetAttractions(goal);
                return response;
            }
            catch (Exception e)
            {
                _logger.Info("Information could not be received");
                return null;
            }
        }

        static AttractionResponseObject GetAttractions(string goal)
        {
            string request = StringPreparer.BuildGoogleRequest(goal);
            Task<string> task = Task.Run<string>(async () => await SendRequest(request));
            string responseString = task.Result;

            return ConvertResponse(responseString);
        }

        static async Task<string> SendRequest(string request)
        {
            HttpResponseMessage response = await client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        static public AttractionResponseObject ConvertResponse(string responseString)
        {
            AttractionResponseObject response = JsonConvert.DeserializeObject<AttractionResponseObject>(responseString);
            return response;
        }
    }
}
