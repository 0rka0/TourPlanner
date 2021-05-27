using System.Collections.Generic;
using TourPlannerDAL.Databases;
using TourPlannerModels;
using TourPlannerModels.TourObject;

namespace TourPlannerTest.TestDatabases
{
    public class TourDatabaseTest : BaseDatabaseHandler
    {
        private static TourDatabaseTest _db;
        public List<Tour> TourList = new List<Tour>();

        TourDatabaseTest() : base(Configuration.TourTable)
        {
            Tour tour = new Tour(1, "1", "Desc", "Inf", "Dist", "1London-Paris.png");
            TourList.Add(tour);

            for(int i = 2; i < 5; i++)
            {
                Tour newTour = tour.Clone();
                newTour.Id = i;
                newTour.Name = i.ToString();
                TourList.Add(newTour);
            }
        }

        public static IDatabase GetInstance()
        {
            if (_db == null)
            {
                _db = new TourDatabaseTest();
            }
            return _db;
        }

        public override void InsertEntry(ITourObject tourObject)
        {
            TourList.Add((Tour)tourObject);
        }

        public override IEnumerable<ITourObject> SelectEntries(int id)
        {
            return TourList;
        }

        public override void UpdateEntry(ITourObject tourObject)
        {
            Tour tour = (Tour)tourObject;
            for (int i = 1; i < TourList.Count; i++)
            {
                if (TourList[i].Id == tour.Id)
                {
                    TourList[i] = tour.Clone();
                }
            }
        }

        public new int GetMaxId()
        {
            int maxId = int.MinValue;

            foreach(Tour tour in TourList)
            {
                if (tour.Id > maxId)
                    maxId = tour.Id;
            }

            return maxId + 1;
        }

        public new void DeleteEntry(int id)
        {
            for(int i = 1; i < TourList.Count; i++)
            {
                if(TourList[i].Id == id)
                {
                    TourList.RemoveAt(i);
                }
            }
        }

        public new void ClearDb()
        {
            TourList.Clear();
        }
    }
}
