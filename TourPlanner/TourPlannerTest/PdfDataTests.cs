using NUnit.Framework;
using TourPlannerTest.TestDatabases;
using TourPlannerDAL.PDF;
using TourPlannerModels;
using TourPlannerModels.TourObject;

namespace TourPlannerTest
{
    public class PdfDataTests
    {
        public TourDatabaseTest tourDb;
        public TourLogDatabaseTest tourLogDb;

        [SetUp]
        public void Setup()
        {
            tourDb = (TourDatabaseTest)TourDatabaseTest.GetInstance();
            tourLogDb = (TourLogDatabaseTest)TourLogDatabaseTest.GetInstance();
        }

        [Test]
        public void GetDetailsAllTours_CreatesPdfModelFromAllTours_ReturnsPdfModel()
        {
            int desiredID1 = tourDb.TourList[0].Id;
            string desiredRep = tourLogDb.TourLogList[0].Report;
            int desiredID2 = tourDb.TourList[1].Id;

            PdfModel model = PdfDataSource.GetDetailsAllTours(tourDb, tourLogDb);

            Assert.AreEqual(desiredID1, model.Tours[0].Id);
            Assert.AreEqual(desiredRep, model.Tours[0].LogList[0].Report);

            Assert.AreEqual(desiredID2, model.Tours[1].Id);
        }

        [Test]
        public void GetDetailsSingleTour_CreatesPdfModelFromSingleTour_ReturnsPdfModel()
        {
            Tour tour = tourDb.TourList[0];
            TourLog tourLog = tourLogDb.TourLogList[0];

            PdfModel model = PdfDataSource.GetDetailsSingleTour(tourLogDb, tour);

            Assert.AreEqual(tour.Id, model.Tours[0].Id);
            Assert.AreEqual(tourLog.Id, model.Tours[0].LogList[0].Id);
        }
    }
}
