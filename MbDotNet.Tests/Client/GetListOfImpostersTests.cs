using System;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;



namespace MbDotNet.Tests.Client
{
    [TestClass, TestCategory("Unit")]
    public class GetListOfImpostersTests : MountebankClientTestBase
    {
        [TestMethod]
        public async Task ImpostersRetrieved_ReturnsImposters()
        {
            await Client.DeleteAllImpostersAsync();
           
            Client.Imposters.Add(new HttpImposter(123, null));
            Client.Imposters.Add(new HttpImposter(456, null));

            var expectedImposters = new List<RetrievedImposters>()
            {
                new RetrievedImposters(){Port=123, Protocol = "Http"},
                new RetrievedImposters(){Port=456, Protocol = "Http"}
            };

            MockRequestProxy.Setup(x => x.GetImpostersAsync(default)).ReturnsAsync(expectedImposters);

            var result = await Client.GetImpostersAsync().ConfigureAwait(false);


            Assert.AreSame(expectedImposters, result);

        }


    }
}

