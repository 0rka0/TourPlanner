using System.Collections.Generic;
using TourPlannerDAL.Databases;
using TourPlannerModels.TourObject;

namespace TourPlannerTest.TestDatabases
{
    public class AttractionDatabaseTest : IDatabase
    {
        public List<Attraction> AttractionList = new List<Attraction>();

        AttractionDatabaseTest()
        {
            for (int i = 0; i < 5; i++)
            {
                Attraction attraction = new Attraction(i+1, "i", "attraction" + (i+1), 4.5f, 200, "adress" + (i+1), (i % 2) + 1);
                AttractionList.Add(attraction);
            }
        }

        public static IDatabase GetInstance()
        {
            return new AttractionDatabaseTest();
        }

        public void InsertEntry(ITourObject tourObject)
        {
            AttractionList.Add((Attraction)tourObject);
        }

        public IEnumerable<ITourObject> SelectEntries(int id)
        {
            List<Attraction> newList = new List<Attraction>();
            foreach(Attraction attraction in AttractionList)
            {
                if (attraction.TourId == id)
                {
                    newList.Add(attraction);
                }
            }
            return newList;
        }

        public int GetMaxId()
        {
            int maxId = int.MinValue;

            foreach (Attraction attraction in AttractionList)
            {
                if (attraction.Id > maxId)
                    maxId = attraction.Id;
            }

            return maxId + 1;
        }

        //not necessary for tests
        public void UpdateEntry(ITourObject tourObject)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteEntry(int id)
        {
            throw new System.NotImplementedException();
        }

        public void ClearDb()
        {
            throw new System.NotImplementedException();
        }
    }
}
