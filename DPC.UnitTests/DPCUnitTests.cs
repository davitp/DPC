using System;
using System.Net.Mime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DPC.UnitTests
{
    /// <summary>
    /// Main unit test class
    /// </summary>
    [DeploymentItem(PathPrefix + @"\" + LanguageDefinitionsPath)]
    [TestClass]
    public class DPCUnitTests
    {
        public DPCUnitTests()
        {
            OutputLocation = AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// Output location
        /// </summary>
        private string OutputLocation { get; set; }

        /// <summary>
        /// Prefix of path containing the XSD and XML definitions for testing
        /// </summary>
        public const string PathPrefix = "Definitions";

        /// <summary>
        /// Language definitions file path
        /// </summary>
        public const string LanguageDefinitionsPath = "LanguageDefinitions.xml"; 

        /// <summary>
        /// Dummy test method
        /// </summary>
        [TestMethod]
        public void DummyTest()
        {
            Assert.IsNotNull(PathPrefix);

            Specification.API.Specification.Initialize(LanguageDefinitionsPath);
        }
    }
}
