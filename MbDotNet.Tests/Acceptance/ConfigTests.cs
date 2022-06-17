using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Acceptance
{
	[TestClass, TestCategory("Acceptance")]
    public class ConfigTests : AcceptanceTestBase
    {
		[TestInitialize]
		public async Task TestInitialize()
		{
			await _client.DeleteAllImpostersAsync();
		}

        [TestMethod]
        public async Task GetConfig()
        {
            var result = await _client.GetConfigAsync();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetConfig_Version()
        {
            const string version = "2.6.0";
            var result = await _client.GetConfigAsync();
            Assert.AreEqual(version, result.Version);
        }

        [TestMethod]
        public async Task GetConfig_Options()
        {
            const bool mock = true;
            var result = await _client.GetConfigAsync();
            Assert.AreEqual(mock, result.Options["mock"]);
        }

        [TestMethod]
        public async Task GetConfig_Process()
        {
            const string nodeversion = "v14.17.2";
            var result = await _client.GetConfigAsync();
            Assert.AreEqual(nodeversion, result.Process["nodeVersion"]);
        }
    }
}