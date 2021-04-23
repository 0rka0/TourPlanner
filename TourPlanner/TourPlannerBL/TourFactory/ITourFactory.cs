using System.Collections.Generic;
using TourPlannerModels;

namespace TourPlannerBL
{
    public interface ITourFactory
    {
        IEnumerable<Tour> GetTours();
        IEnumerable<Tour> Search(string filter);
    }
}
