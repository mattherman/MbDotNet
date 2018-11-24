using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using FluentAssertions;
using MbDotNet.Enums;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanCreateTcpProxyImposter : AcceptanceTest
    {
        private const int SourceImposterPort = 6000;
        private const int ProxyImposterPort = 6001;
        private RetrievedTcpImposter _retrievedImposter;

        public override void Run()
        {
            DeleteAllImposters();
            CreateSourceImposter();
            CreateProxyImposter();
            MakeRequestToImposter();
            GetSourceImposter();
            VerifyRequestProxiedToSource();
            DeleteAllImposters();
        }

        private void GetSourceImposter()
        {
            _retrievedImposter = _client.GetTcpImposterAsync(SourceImposterPort);
        }

        private void DeleteAllImposters()
        {
            _client.DeleteAllImpostersAsync();
        }

        private void VerifyRequestProxiedToSource()
        {
            _retrievedImposter.Should().NotBeNull("Expected imposter to have been retrieved");
            _retrievedImposter.NumberOfRequests.Should().Be(1);
        }

        private void CreateSourceImposter()
        {
            var imposter = _client.CreateTcpImposter(SourceImposterPort);
            imposter.AddStub().ReturnsData("abc123");
            _client.SubmitAsync(imposter);
        }

        private void CreateProxyImposter()
        {
            var proxyImposter = _client.CreateTcpImposter(ProxyImposterPort);

            var predicateGenerators = new List<MatchesPredicate<TcpBooleanPredicateFields>>
            {
                new MatchesPredicate<TcpBooleanPredicateFields>(new TcpBooleanPredicateFields
                {
                    Data = true
                })
            };

            proxyImposter.AddStub().ReturnsProxy(
                new System.Uri($"tcp://localhost:{SourceImposterPort}"), 
                ProxyMode.ProxyOnce, predicateGenerators);

            _client.SubmitAsync(proxyImposter);
        }

        private void MakeRequestToImposter()
        {
            using (var client = new TcpClient("localhost", ProxyImposterPort))
            {
                var data = Encoding.ASCII.GetBytes("testdata");
                using (var stream = client.GetStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
        }
    }
}
