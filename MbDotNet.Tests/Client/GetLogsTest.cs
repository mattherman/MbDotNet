using System;
using MbDotNet.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MbDotNet.Tests.Client

{
    [TestClass, TestCategory("Unit")]

    public class GetLogsTest : MountebankClientTestBase
    {
        [TestMethod]

        public async Task ReturnsLogs()
        {

            var expectedResult = new List<Log>()
            {
                new Log(){Level="info",Message="[mb:2525] DELETE /imposters",Timestamp=""},
                new Log(){Level="warn",Message="",Timestamp=""},
                new Log(){Level="info",Message="[mb:2525] GET /imposters",Timestamp=""},
               
            };
           
           
            MockRequestProxy.Setup(x => x.GetLogsAsync(default)).ReturnsAsync(expectedResult);
            var result = await Client.GetLogsAsync().ConfigureAwait(false);

            Assert.AreSame(expectedResult, result);
        }
    }
}



