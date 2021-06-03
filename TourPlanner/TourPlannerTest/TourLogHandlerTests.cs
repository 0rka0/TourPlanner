using NUnit.Framework;
using System.Collections.Generic;
using TourPlannerBL.TourObjectHandling;
using TourPlannerModels.TourObject;
using TourPlannerTest.TestDatabases;

namespace TourPlannerTest
{
    public class TourLogHandlerTests
    {
        public TourLogDatabaseTest db;

        [SetUp]
        public void Setup()
        {
            db = (TourLogDatabaseTest)TourLogDatabaseTest.GetInstance();
            TourLogHandler.Init(db);
        }

        [Test]
        public void DelTourLog_DeletingASingleTourLogFromDB_ReduceContentOfDbByOne()
        {
            TourLog tourLog = new TourLog();
            tourLog.Id = 2;
            int countOld = db.TourLogList.Count;
            TourLogHandler.DelTourLog(tourLog.Id);

            Assert.AreNotEqual(db.TourLogList.Count, countOld);
        }

        [Test]
        public void DelTourLog_DeletingASingleTourLogFromDB_DeletedTourLogNoLongerInDb()
        {
            bool stillInDb = false;

            TourLog tourLog = new TourLog();
            tourLog.Id = 2;
            TourLogHandler.DelTourLog(tourLog.Id);

            foreach (TourLog curTour in db.TourLogList)
            {
                if (curTour.Id == tourLog.Id)
                {
                    stillInDb = true;
                }
            }

            Assert.IsFalse(stillInDb);
        }

        [Test]
        public void EditTourLogs_UpdatingTourLogList_ChangesAppliedToTourLogsInDb()
        {
            string originalRep = "Report1";
            string desRep1 = "desired string 1";
            string desRep2 = "desired string 2";

            List<TourLog> tourLogList = new List<TourLog>();
            tourLogList.AddRange(db.TourLogList);
            tourLogList[1].Report = desRep1;
            tourLogList[2].Report = desRep2;

            TourLogHandler.EditTourLogs(tourLogList);

            Assert.AreEqual(db.TourLogList[0].Report, originalRep);
            Assert.AreEqual(db.TourLogList[1].Report, desRep1);
            Assert.AreEqual(db.TourLogList[2].Report, desRep2);
        }

        [Test]
        public void AddNewTourLog_InsertingTourLogIntoDb_NewTourLogInserted()
        {
            int countOld = db.TourLogList.Count;
            TourLogHandler.AddNewTourLog(1);

            Assert.AreNotEqual(countOld, db.TourLogList.Count);
        }

        [Test]
        public void AddNewTourLog_InsertingTourLogIntoDb_NewTourLogInsertedWithCorrectId()
        {
            int maxIdOld = int.MinValue;

            foreach (TourLog curTourLog in db.TourLogList)
            {
                if (curTourLog.Id > maxIdOld)
                {
                    maxIdOld = curTourLog.Id;
                }
            }

            TourLogHandler.AddNewTourLog(1);

            int maxIdNew = db.TourLogList[db.TourLogList.Count - 1].Id;

            Assert.AreNotEqual(maxIdOld, maxIdNew);
            Assert.AreEqual(maxIdOld + 1, maxIdNew);
        }

        [Test]
        public void AddImportedTourLog_InsertingTouLogIntoDb_ExistingTourLogInserted()
        {
            TourLog tourLog = new TourLog();
            tourLog.Id = 6;

            TourLogHandler.AddImportedTourLog(tourLog);

            Assert.AreEqual(tourLog.Id, db.TourLogList[db.TourLogList.Count - 1].Id);
        }

        [Test]
        public void ClearData_DeletingAllEntriesFromDb_DbEmpty()
        {
            TourLogHandler.ClearData();

            Assert.IsEmpty(db.TourLogList);
        }
    }
}
