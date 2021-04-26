using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerModels
{
    public class Configuration
    {
        public static readonly string ImagePath = @"..\..\..\Images\";
        public static readonly string ConnectionString = "Host=localhost;Username=postgres;Password=postgres;Database=TourPlanner";

        Configuration()
        {
            
        }
    }
}
