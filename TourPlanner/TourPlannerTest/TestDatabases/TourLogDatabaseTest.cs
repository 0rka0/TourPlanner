using System;
using System.Collections.Generic;
using TourPlannerDAL.Databases;
using TourPlannerModels.TourObject;
using TourPlannerModels.Types;

namespace TourPlannerTest.TestDatabases
{
    public class TourLogDatabaseTest : IDatabase
    {
        public List<TourLog> TourLogList = new List<TourLog>();

        TourLogDatabaseTest()
        {
            for (int i = 1; i < 5; i++)
            {
                TourLog tourLog = new TourLog(i, DateTime.Now, 50, "Report" + i.ToString(), 1, (int)Ratings.Good, 100, (int)WeatherTypes.Sunny, (int)TrafficTypes.Medium, 1, 1, i);
                TourLogList.Add(tourLog);
            }
            TourLog tourLog2 = new TourLog(5, DateTime.Now, 50, "Report" + 5.ToString(), 1, (int)Ratings.Good, 100, (int)WeatherTypes.Sunny, (int)TrafficTypes.Medium, 1, 1, 1);
            TourLogList.Add(tourLog2);
        }

        public static IDatabase GetInstance()
        {
            return new TourLogDatabaseTest();
        }

        public void InsertEntry(ITourObject tourObject)
        {
            TourLogList.Add((TourLog)tourObject);
        }

        public IEnumerable<ITourObject> SelectEntries(int id)
        {
            List<TourLog> newList = new List<TourLog>();
            foreach (TourLog tourLog in TourLogList)
            {
                if (tourLog.TourId == id)
                {
                    newList.Add(tourLog);
                }
            }
            return newList;
        }

        public void UpdateEntry(ITourObject tourObject)
        {
            TourLog tourLog = (TourLog)tourObject;
            for (int i = 1; i < TourLogList.Count + 1; i++)
            {
                if (TourLogList[i].Id == tourLog.Id)
                {
                    TourLogList[i] = tourLog;
                }
            }
        }

        public int GetMaxId()
        {
            int maxId = int.MinValue;

            foreach (TourLog tourLog in TourLogList)
            {
                if (tourLog.Id > maxId)
                    maxId = tourLog.Id;
            }

            return maxId + 1;
        }

        public void DeleteEntry(int id)
        {
            for (int i = 1; i < TourLogList.Count + 1; i++)
            {
                if (TourLogList[i].Id == id)
                {
                    TourLogList.RemoveAt(i);
                }
            }
        }

        public void ClearDb()
        {
            TourLogList.Clear();
        }
    }
}
