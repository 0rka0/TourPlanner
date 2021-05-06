using NUnit.Framework;
using System;
using TourPlannerBL.StringPrep;

namespace TourPlannerTest
{
    public class StringPreparerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BuildRequest_MapQuestDirectionsRequest_ReturnsStringWithStartAndGoal()
        {
            string loc1 = "Wien";
            string loc2 = "Graz";

            string desiredString = $"http://www.mapquestapi.com/directions/v2/route?key=A1H6TsijwzAZ3cp7vu5cGAmVqEysE6gy&from={loc1}&to={loc2}";
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
        public void BuildName_BuildingPdfName_ReturnsStringAsValidFilename()
        {
            DateTime date = DateTime.Now;

            string desiredString = $"Report_{date.ToString("yyyy_MM_dd_HH_mm_ss")}.pdf";
            string actualString = StringPreparer.BuildReportName(date);

            Assert.AreEqual(desiredString, actualString);
        }
    }
}