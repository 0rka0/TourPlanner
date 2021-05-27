using TourPlannerDAL.Databases;

namespace TourPlannerBL.TourObjectHandling
{
    static public class Initiator
    {
        static public void Init()
        {
            TourHandler.Init(TourDatabaseHandler.GetInstance());
            TourLogHandler.Init(TourLogDatabaseHandler.GetInstance());
        }
    }
}
