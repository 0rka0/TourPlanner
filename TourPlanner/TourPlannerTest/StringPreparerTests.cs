using NUnit.Framework;
using System;
using System.Configuration;
using TourPlannerBL.StringPrep;

namespace TourPlannerTest
{
    public class StringPreparerTests
    {
        [SetUp]
        public void Setup()
        {
            TourPlannerModels.Configuration.Configure(ConfigurationManager.AppSettings);
        }

        [Test]
        public void BuildRequest_MapQuestDirectionsRequest_ReturnsStringWithStartAndGoal()
        {
            string loc1 = "Wien";
            string loc2 = "Graz";

            string desiredString = $"http://www.mapquestapi.com/directions/v2/route?key={TourPlannerModels.Configuration.Key}&from={loc1}&to={loc2}";
            string actualString = StringPreparer.BuildRequest(loc1, loc2);

            Assert.AreEqual(desiredString, actualString);
        }

        [Test]
        public void BuildName_BuildingATourname_ReturnsStringWithStartAndGoal()
        {
            string loc1 = "Wien";
            string loc2 = "Graz";

            string desiredString = $"{loc1}-{loc2}";
            string actualString = StringPreparer.BuildName(loc1, loc2);

            Assert.AreEqual(desiredString, actualString);
        }

        [Test]
        public void BuildFilename_BuildingFilenameForImage_ReturnsStringAsValidFilename()
        {
            int id = 2;
            string loc1 = "Wien";
            string loc2 = "Graz";
            
            string desiredString = $"{id}{loc1}-{loc2}.png";
            string actualString = StringPreparer.BuildFilename(id, StringPreparer.BuildName(loc1, loc2));

            Assert.AreEqual(desiredString, actualString);
        }

        [Test]
        public void BuildName_BuildingReportName_ReturnsStringAsValidFilename()
        {
            DateTime date = DateTime.Now;

            string desiredString = $"Report_{date.ToString("yyyy_MM_dd_HH_mm_ss")}.pdf";
            string actualString = StringPreparer.BuildReportName(date);

            Assert.AreEqual(desiredString, actualString);
        }

        [Test]
        public void BuildName_BuildingSummaryName_ReturnsStringAsValidFilename()
        {
            DateTime date = DateTime.Now;

            string desiredString = $"Summary_{date.ToString("yyyy_MM_dd_HH_mm_ss")}.pdf";
            string actualString = StringPreparer.BuildSummaryName(date);

            Assert.AreEqual(desiredString, actualString);
        }

        [Test]
        public void ExtractLocationFromFilename_ExtractsStartAndGoalFromFilename_ReturnsTupleOfTwoStrings()
        {
            Tuple<string, string> desiredTuple = Tuple.Create("Wien", "Graz");
            Tuple<string, string> actualTuple = StringPreparer.ExtractLocationFromFilename("1Wien-Graz.png");
            
            Assert.AreEqual(desiredTuple, actualTuple);
        }

        [Test]
        public void BuildGoogleRequest_BuildingGoogleRequest_ReturnsStringWithGoal()
        {
            string goal = "Wien";
            string desiredString = $"https://maps.googleapis.com/maps/api/place/textsearch/json?query={goal}+point+of+interest&key={TourPlannerModels.Configuration.GoogleKey}";
            string actualString = StringPreparer.BuildGoogleRequest(goal);

            Assert.AreEqual(desiredString, actualString);
        }

    }
}