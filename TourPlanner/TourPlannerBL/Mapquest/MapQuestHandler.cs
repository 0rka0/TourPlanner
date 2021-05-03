using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TourPlannerModels;
using TourPlannerDAL.Files;
using log4net;
using System.Reflection;
using TourPlannerBL.StringPrep;
using System;

namespace TourPlannerBL.Mapquest
{
    //used to interact with the MapQuest API
    static public class MapQuestHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static readonly HttpClient client = new HttpClient();

        static public TourInformationResponseObject GetTourInformation(string start, string goal)
        {
            _logger.Info("Requesting Tour Information from MapQuest");

            TourInformationResponseObject response = GetTour(start, goal);
            return response;
        }

        static TourInformationResponseObject GetTour(string start, string goal)
        {
            string request = StringPreparer.BuildRequest(start, goal);
            Task<string> task = Task.Run<string>(async () => await RequestTourInformation(request));
            string directionsString = task.Result; //response from MapQuest API

            return ConvertResponse(directionsString);
        }

        static public void GetImage(TourInformationResponseObject response, string filename)
        {
            _logger.Info("Requesting Image from MapQuest");

            string request = StringPreparer.BuildRequest(response.ReturnString());
            Task task = Task.Run(async () => await DownloadAndSaveImage(request, filename));
        }

        static TourInformationResponseObject ConvertResponse(string directionsString)
        {
            TourInformationResponseObject response = JsonConvert.DeserializeObject<TourInformationResponseObject>(directionsString);
            return response;
        }

        static async Task<string> RequestTourInformation(string request)
        {
            HttpResponseMessage response = await client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        static async Task DownloadAndSaveImage(string request, string filename)
        {
                string path = Configuration.ImagePath + filename;
                HttpResponseMessage response = await client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                await using Stream ms = await response.Content.ReadAsStreamAsync();
                await FileHandler.SaveImage(ms, path);
        }
    }
}
