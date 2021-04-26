﻿using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TourPlannerModels;

namespace TourPlannerBL
{
    //used to interact with the MapQuest API
    static public class MapQuestHandler
    {
        static readonly HttpClient client = new HttpClient();

        static public TourInformationResponse GetTourInformation(string start, string goal)
        {
            TourInformationResponse response = GetTour(start, goal);
            return response;
        }

        static TourInformationResponse GetTour(string start, string goal)
        {
            string request = StringPreparer.BuildRequest(start, goal);
            Task<string> task = Task.Run<string>(async () => await RequestTourInformation(request));
            string directionsString = task.Result; //response from MapQuest API

            return ConvertResponse(directionsString);
        }

        static public string GetImage(TourInformationResponse response, string filename)
        {
            string request = StringPreparer.BuildRequest(response.ReturnString());
            Task<string> task = Task.Run<string>(async () => await DownloadAndSaveImage(request, filename));
            string fname = task.Result;

            return fname; //return location
        }

        static TourInformationResponse ConvertResponse(string directionsString)
        {
            TourInformationResponse response = JsonConvert.DeserializeObject<TourInformationResponse>(directionsString);
            return response;
        }

        static async Task<string> RequestTourInformation(string request)
        {
            HttpResponseMessage response = await client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        static async Task<string> DownloadAndSaveImage(string request, string filename)
        {
            string path = Configuration.ImagePath + filename;
            HttpResponseMessage response = await client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            await using var ms = await response.Content.ReadAsStreamAsync();
            await using var fs = File.Create(path);
            ms.Seek(0, SeekOrigin.Begin);
            ms.CopyTo(fs);

            return filename;
        }
    }
}
