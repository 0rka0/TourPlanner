using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using TourPlannerBL.API.GooglePlaces;
using TourPlannerBL.TourObjectHandling;
using TourPlannerModels.TourObject;
using TourPlannerTest.TestDatabases;

namespace TourPlannerTest
{
    public class AttractionHandlerTests
    {
        public AttractionDatabaseTest db;
        string attractionString = string.Empty;
        AttractionResponseObject attractions;

        [SetUp]
        public void Setup()
        {
            db = (AttractionDatabaseTest)AttractionDatabaseTest.GetInstance();
            AttractionHandler.Init(db);
            attractionString = File.ReadAllText(Path.GetFullPath("../../../test.json"));
            attractions = GooglePlacesHandler.ConvertResponse(attractionString);
        }

        [Test]
        public void AddNewAttractions_InsertingAttractionsIntoDb_NewAttractionsInserted()
        {
            int countOld = db.AttractionList.Count;
            AttractionHandler.AddNewAttractions(attractions, 1);

            Assert.AreNotEqual(countOld, db.AttractionList.Count);
        }

        [Test]
        public void AddNewAttractions_InsertingAttractionsIntoDb_NewAttractionsInsertedWithCorrectId()
        {
            int maxIdOld = int.MinValue;

            foreach (Attraction attraction in db.AttractionList)
            {
                if (attraction.Id > maxIdOld)
                    maxIdOld = attraction.Id;
            }

            AttractionHandler.AddNewAttractions(attractions, 1);

            int maxIdNew = db.AttractionList[db.AttractionList.Count - 1].Id;

            Assert.AreNotEqual(maxIdOld, maxIdNew);
            Assert.AreEqual(maxIdOld + attractions.results.Count, maxIdNew);
        }

        [Test]
        public void AddImportedAttractions_InsertingAttractionsIntoDb_ExistingAttractionsInserted()
        {
            List<Attraction> newList = new List<Attraction>();
            Attraction att1 = new Attraction();
            att1.Id = 7;
            Attraction att2 = new Attraction();
            att2.Id = 9;
            newList.Add(att1);
            newList.Add(att2);

            AttractionHandler.AddImportedAttractions(newList);

            Assert.AreEqual(att1.Id, db.AttractionList[db.AttractionList.Count - 2].Id);
            Assert.AreEqual(att2.Id, db.AttractionList[db.AttractionList.Count - 1].Id);
        }
    }
}
