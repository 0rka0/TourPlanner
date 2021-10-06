using TourPlannerDAL.Databases;
using TourPlannerDAL.Files;
using TourPlannerBL.TourObjectHandling;

namespace TourPlannerBL
{
    static public class AppInit
    {
        static public void Init()
        {
            IDatabase tourDb = TourDatabaseHandler.GetInstance();
            IDatabase tourLogDb = TourLogDatabaseHandler.GetInstance();
            //IDatabase attractionDb = AttractionDatabaseHandler.GetInstance();
            TourHandler.Init(tourDb);
            TourSelector.Init(tourDb);
            TourLogHandler.Init(tourLogDb);
            TourLogSelector.Init(tourLogDb);
            //AttractionHandler.Init(attractionDb);
            //AttractionSelector.Init(attractionDb);

            FileHandler.Init();
        }
    }
}
