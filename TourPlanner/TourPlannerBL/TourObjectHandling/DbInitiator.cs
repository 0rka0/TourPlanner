using TourPlannerDAL.Databases;

namespace TourPlannerBL.TourObjectHandling
{
    static public class DbInitiator
    {
        static public void Init()
        {
            //needs to be fixed
            IDatabase tourDb = TourDatabaseHandler.GetInstance();
            IDatabase tourLogDb = TourLogDatabaseHandler.GetInstance();
            TourHandler.Init(tourDb);
            TourSelector.Init(tourDb);
            TourLogHandler.Init(tourLogDb);
            TourLogSelector.Init(tourLogDb);
        }
    }
}
