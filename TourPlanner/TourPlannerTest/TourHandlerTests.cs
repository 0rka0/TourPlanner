using NUnit.Framework;
using System.Collections.Generic;
using TourPlannerBL.TourObjectHandling;
using TourPlannerModels.TourObject;
using TourPlannerTest.TestDatabases;

namespace TourPlannerTest
{
    public class TourHandlerTests
    {
        public TourDatabaseTest db;

        [SetUp]
        public void Setup()
        {
            db = (TourDatabaseTest)TourDatabaseTest.GetInstance();
            TourHandler.Init(db);
        }

        [Test]
        public void DeleteTour_DeletingASingleTourFromDB_ReduceContentOfDbByOne()
        {
            Tour tour = new Tour();
            tour.Id = 2;
            int countOld = db.TourList.Count;
            TourHandler.DeleteTour(tour);

            Assert.AreNotEqual(db.TourList.Count, countOld);
        }

        [Test]
        public void DeleteTour_DeletingASingleTourFromDB_DeletedTourNoLongerInDb()
        {
            bool stillInDb = true;

            Tour tour = new Tour();
            tour.Id = 2;
            TourHandler.DeleteTour(tour);

            foreach(Tour curTour in db.TourList)
            {
                if(curTour.Id == tour.Id)
                {
                    stillInDb = false;
                }
            }

            Assert.IsTrue(stillInDb);
        }

        [Test]
        public void EditTour_EditingTourInDB_NameChangesAppliedInDb()
        {
            string desiredValue = "desiredName";
            string actualValue = string.Empty;


            Tour tour = new Tour();
            tour.Id = 2;
            TourHandler.EditTour(desiredValue, "", "", tour);

            foreach (Tour curTour in db.TourList)
            {
                if (curTour.Id == tour.Id)
                {
                    actualValue = curTour.Name;
                    break;
                }
            }

            Assert.AreEqual(desiredValue, actualValue);
        }

        [Test]
        public void EditTour_EditingTourInDB_DescriptionChangesAppliedInDb()
        {
            string desiredValue = "desiredName";
            string actualValue = string.Empty;

            Tour tour = new Tour();
            tour.Id = 2;
            TourHandler.EditTour("", desiredValue, "", tour);

            foreach (Tour curTour in db.TourList)
            {
                if (curTour.Id == tour.Id)
                {
                    actualValue = curTour.TourDescription;
                    break;
                }
            }

            Assert.AreEqual(desiredValue, actualValue);
        }

        [Test]
        public void EditTour_EditingTourInDB_InformationChangesAppliedInDb()
        {
            string desiredValue = "desiredName";
            string actualValue = string.Empty;

            Tour tour = new Tour();
            tour.Id = 2;
            TourHandler.EditTour("", "", desiredValue, tour);

            foreach (Tour curTour in db.TourList)
            {
                if (curTour.Id == tour.Id)
                {
                    actualValue = curTour.RouteInformation;
                    break;
                }
            }

            Assert.AreEqual(desiredValue, actualValue);
        }

        [Test]
        public void CopyTour_CopyingTourFromDb_CopyInsertedIntoDb()
        {
            Tour tour = db.TourList[2];
            int countOld = db.TourList.Count;
            TourHandler.CopyTour(tour);

            Assert.AreNotEqual(countOld, db.TourList.Count);
        }

        [Test]
        public void CopyTour_CopyingTourFromDb_CopyHasCorrectValues()
        {
            Tour tour = db.TourList[2];
            TourHandler.CopyTour(tour);

            Tour copy = db.TourList[db.TourList.Count - 1];
            Assert.AreEqual(tour.Name, copy.Name);
        }

        [Test]
        public void InsertTour_InsertingTourIntoDb_NewTourInserted()
        {
            Tour tour = new Tour();
            int countOld = db.TourList.Count;
            TourHandler.InsertTour(tour, true);

            Assert.AreNotEqual(countOld, db.TourList.Count);
        }

        [Test]
        public void InsertTour_InsertingTourIntoDb_NewTourInsertedWithCorrectId()
        {
            int maxIdOld = int.MinValue;
            Tour tour = new Tour();

            foreach (Tour curTour in db.TourList)
            {
                if (curTour.Id > maxIdOld)
                {
                    maxIdOld = curTour.Id;
                }
            }

            TourHandler.InsertTour(tour, true);

            int maxIdNew = db.TourList[db.TourList.Count - 1].Id;

            Assert.AreNotEqual(maxIdOld, maxIdNew);
        }

        [Test]
        public void InsertTour_InsertingTourIntoDb_ExistingTourInserted()
        {
            Tour tour = new Tour();
            tour.Id = 6;

            TourHandler.InsertTour(tour, false);

            Assert.AreEqual(tour.Id, db.TourList[db.TourList.Count - 1].Id);
        }

        //ClearDataTest to be added after TourLog testing is included
    }
}
