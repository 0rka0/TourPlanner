using System.Collections.Generic;
using TourPlannerModels;

namespace TourPlannerBL
{
    public interface ITourFactory
    {
        IEnumerable<Tour> GetAllTours();
        IEnumerable<Tour> Search(string filter);
    }
}
