
namespace TourPlannerBL
{
    public static class TourFactory
    {
        private static ITourFactory _tourFactory;

        public static ITourFactory GetInstance()
        {
            if (_tourFactory == null)
            {
                _tourFactory = new TourFactoryImpl();
            }
            return _tourFactory;
        }
    }
}
