using System.Collections.Specialized;

namespace TourPlannerModels
{
    public static class Configuration
    {
        public static string ImagePath { get; private set; }
        public static string ReportPath { get; private set; }
        public static string ConnectionString { get; private set; }
        public static string TourTable { get; private set; }
        public static string TourLogTable { get; private set; }
        public static string AttractionTable { get; private set; }
        public static string UrlDirectionsApi { get; private set; }
        public static string UrlStaticMapApi { get; private set; }
        public static string UrlGooglePlacesApi { get; private set; }
        public static string Key { get; private set; }
        public static string GoogleKey { get; private set; }

        public static void Configure(NameValueCollection configData)
        {
            ImagePath = configData.Get(nameof(ImagePath));
            ReportPath = configData.Get(nameof(ReportPath));
            ConnectionString = configData.Get(nameof(ConnectionString));
            TourTable = configData.Get(nameof(TourTable));
            TourLogTable = configData.Get(nameof(TourLogTable));
            AttractionTable = configData.Get(nameof(AttractionTable));
            UrlDirectionsApi = configData.Get(nameof(UrlDirectionsApi));
            UrlStaticMapApi = configData.Get(nameof(UrlStaticMapApi));
            UrlGooglePlacesApi = configData.Get(nameof(UrlGooglePlacesApi));
            Key = configData.Get(nameof(Key));
            GoogleKey = configData.Get(nameof(GoogleKey));
        }
    }
}
