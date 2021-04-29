namespace TourPlannerModels
{
    public static class Configuration
    {
        public static readonly string ImagePath = @"..\..\..\Images\";
        public static readonly string ReportPath = @"..\..\..\Reports\";
        public static readonly string ConnectionString = "Host=localhost;Username=postgres;Password=postgres;Database=TourPlanner";
        public static readonly string TourTable = "tours";
        public static readonly string TourLogTable = "tourlogs";
    }
}
