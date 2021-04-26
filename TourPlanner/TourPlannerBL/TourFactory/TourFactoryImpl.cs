using System.Collections.Generic;
using System.Linq;
using TourPlannerModels;

namespace TourPlannerBL
{
    class TourFactoryImpl : ITourFactory
    {
        public IEnumerable<Tour> GetAllTours()
        {
            return TourHandler.GetTours();
        }

        public IEnumerable<Tour> Search(string filter)
        {
            IEnumerable<Tour> tours = GetAllTours();

            return tours.Where(x => x.Name.ToLower().Contains(filter.ToLower()));
        }
    }
}
