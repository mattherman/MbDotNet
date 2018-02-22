using MbDotNet.Enums;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MbDotNet.FunctionalTests
{
    [TestClass]
    public class CreateProxy
    {
        [TestMethod]
        public void CreateProxyTest()
        {
            MountebankClient mbc = new MountebankClient();

            var imposter = mbc.CreateHttpImposter(4999, "TomNugetTest");

            imposter.RecordRequests = true;
                           

            

            var mainpredicate = new ContainsPredicate<HttpPredicateFields>(new HttpPredicateFields
                                                                        {
                                                                            Path = "sometestPath"
                                                                        });


            var generatorPredicate = new MatchesPredicate<PredicateGeneratorFields>(new PredicateGeneratorFields { Path = true, Method = true, QueryParameters = true });

            imposter
                .AddStub()
                 .On(mainpredicate)
                .Returns(new Uri("https://problematicordersapi-qa9.bapps.je-labs.com"), ProxyMode.ProxyOnce, new List<PredicateBase>() { generatorPredicate } );

            mbc.Submit(imposter);
        }
    }
}
