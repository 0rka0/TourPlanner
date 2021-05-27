using NUnit.Framework;
using TourPlannerBL.TourObjectHandling;
using TourPlannerTest.TestDatabases;

namespace TourPlannerTest
{
    public class TourHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            TourHandler.Init(TourDatabaseTest.GetInstance());
        }

        [Test]
        public void Test1()
        {
            //TourHandler.
        }
    }
}
