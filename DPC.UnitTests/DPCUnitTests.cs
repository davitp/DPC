using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DPC.UnitTests
{
    /// <summary>
    /// Main unit test class
    /// </summary>
    [TestClass]
    public class DPCUnitTests
    {
        /// <summary>
        /// Prefix of path containing the XSD and XML definitions for testing
        /// </summary>
        private const string PathPrefix = "Definitions";

        /// <summary>
        /// Dummy test method
        /// </summary>
        [TestMethod]
        public void DummyTest()
        {
            Assert.IsNotNull(PathPrefix);
        }
    }
}
