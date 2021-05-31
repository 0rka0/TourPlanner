using NUnit.Framework;
using System.Collections.Generic;
using TourPlannerBL.TourObjectHandling;
using TourPlannerModels.TourObject;
using TourPlannerTest.TestDatabases;

namespace TourPlannerTest
{
    class SelectorTests
    {
        public TourDatabaseTest tourDb;
        public TourLogDatabaseTest tourLogDb;

        [SetUp]
        public void Setup()
        {
            tourDb = (TourDatabaseTest)TourDatabaseTest.GetInstance();
            TourSelector.Init(tourDb);
            tourLogDb = (TourLogDatabaseTest)TourLogDatabaseTest.GetInstance();
            TourLogSelector.Init(tourLogDb);
        }

        [Test]
        public void SelectTourLogsById_SelectingTourLogToDefinedTour_ReturnsListOfTourLogs()
        {
            int id1 = 2;
            int id2 = 1;
            int desiredAmount1 = 1;
            int desiredAmount2 = 2;

            List<TourLog> list1 = (List<TourLog>)TourLogSelector.SelectTourLogsById(id1);
            List<TourLog> list2 = (List<TourLog>)TourLogSelector.SelectTourLogsById(id2);

            Assert.AreEqual(desiredAmount1, list1.Count);
            Assert.AreEqual(desiredAmount2, list2.Count);
        }

        [Test]
        public void GetTours_SelectingAllToursFromDb_ReturnsListWithCorrectAmountOfTours()
        {
            int desiredAmount = tourDb.TourList.Count;

            List<Tour> list = (List<Tour>)TourSelector.GetTours();

            Assert.AreEqual(desiredAmount, list.Count);
        }

        [Test]
        public void GetTours_SelectingAllToursFromDb_ReturnsCorrectListOfTours()
        {
            int desiredID1 = tourDb.TourList[0].Id;
            int desiredID2 = tourDb.TourList[2].Id;

            List<Tour> list = (List<Tour>)TourSelector.GetTours();

            Assert.AreEqual(desiredID1, list[0].Id);
            Assert.AreEqual(desiredID2, list[2].Id);
        }

        [Test]
        public void GetTours_SelectingAllToursFromDb_AddsCorrespondingTourLogsToTour()
        {
            int desiredAmount1 = 2;
            int desiredAmount2 = 1;

            List<Tour> list = (List<Tour>)TourSelector.GetTours();

            Assert.AreEqual(desiredAmount1, list[0].LogList.Count);
            Assert.AreEqual(desiredAmount2, list[2].LogList.Count);
        }

        [Test]
        public void Search_SelectsToursThatFitCriteria_ReturnsCorrectListOfTours()
        {
            string filter1 = "1";
            string filter2 = "200";
            int desiredAmount = 1;

            IEnumerable<Tour> tmplist1 = TourSelector.Search(filter1);
            List<Tour> list1 = new List<Tour>();

            foreach (Tour tour in tmplist1)
                list1.Add(tour);

            IEnumerable<Tour> list2 = TourSelector.Search(filter2);

            Assert.AreEqual(desiredAmount, list1.Count);
            Assert.IsEmpty(list2);
        }
    }
}
