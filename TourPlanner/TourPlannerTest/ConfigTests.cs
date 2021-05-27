using NUnit.Framework;
using System;
using System.Configuration;
using System.Collections.Specialized;

namespace TourPlannerTest
{
    [TestFixture]
    class ConfigTests
    {
        [SetUp]
        public void Setup()
        {
            TourPlannerModels.Configuration.Configure(ConfigurationManager.AppSettings);
        }

        [Test]
        public void VerfifyConfigSettings()
        {
            string actualString = ConfigurationManager.AppSettings["Key"];
            Assert.IsFalse(String.IsNullOrEmpty(actualString), "No App.Config found");
        }

        [Test]
        public void Configure_LoadsAppSettingsFromConfigFile_CorrectSettingInConfigurationClass()
        {
            string actualString = TourPlannerModels.Configuration.Key;
            Assert.AreEqual("tours", TourPlannerModels.Configuration.TourTable);
        }
    }
}
