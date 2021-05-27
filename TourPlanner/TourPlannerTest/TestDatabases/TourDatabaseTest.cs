using System.Collections.Generic;
using TourPlannerDAL.Databases;
using TourPlannerModels;
using TourPlannerModels.TourObject;

namespace TourPlannerTest.TestDatabases
{
    public class TourDatabaseTest : IDatabase
    {
        private static TourDatabaseTest _db;
        public List<Tour> TourList = new List<Tour>();

        TourDatabaseTest()
        {
            Tour tour = new Tour(1, "1", "Desc", "Inf", "Dist", "0Moped-Banane.png");
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
            return new TourDatabaseTest();
        }

        public void InsertEntry(ITourObject tourObject)
        {
            TourList.Add((Tour)tourObject);
        }

        public IEnumerable<ITourObject> SelectEntries(int id)
        {
            return TourList;
        }

        public void UpdateEntry(ITourObject tourObject)
        {
            Tour tour = (Tour)tourObject;
            for (int i = 1; i < TourList.Count+1; i++)
            {
                if (TourList[i].Id == tour.Id)
                {
                    TourList[i] = tour.Clone();
                }
            }
        }

        public int GetMaxId()
        {
            int maxId = int.MinValue;

            foreach(Tour tour in TourList)
            {
                if (tour.Id > maxId)
                    maxId = tour.Id;
            }

            return maxId + 1;
        }

        public void DeleteEntry(int id)
        {
            for(int i = 1; i < TourList.Count+1; i++)
            {
                if(TourList[i].Id == id)
                {
                    TourList.RemoveAt(i);
                }
            }
        }

        public void ClearDb()
        {
            TourList.Clear();
        }
    }
}
