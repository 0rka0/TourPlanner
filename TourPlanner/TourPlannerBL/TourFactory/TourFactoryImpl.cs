using System.Collections.Generic;
using System.Linq;
using TourPlannerModels;

namespace TourPlannerBL
{
    class TourFactoryImpl : ITourFactory
    {
        public IEnumerable<Tour> GetTours()
        {
            return TourHandler.GetTours();
                //new List<Tour>() { new Tour("1", "1", "1", "1"), new Tour("2", "2", "2", "2"), new Tour("4", "4", "4", "4"), new Tour("3", "3", "3", "3") };
        }

        public IEnumerable<Tour> Search(string filter)
        {
            IEnumerable<Tour> tours = GetTours();

            return tours.Where(x => x.Name.ToLower().Contains(filter.ToLower()));
        }
    }
}
