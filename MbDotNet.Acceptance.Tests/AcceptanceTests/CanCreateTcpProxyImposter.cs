using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
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

        public override async Task Run()
        {
            await DeleteAllImposters().ConfigureAwait(false);
            await CreateSourceImposter().ConfigureAwait(false);
            await CreateProxyImposter().ConfigureAwait(false);
            await MakeRequestToImposter().ConfigureAwait(false);
            await GetSourceImposter().ConfigureAwait(false);
            VerifyRequestProxiedToSource();
            await DeleteAllImposters().ConfigureAwait(false);
        }

        private async Task GetSourceImposter()
        {
            _retrievedImposter = await _client.GetTcpImposterAsync(SourceImposterPort).ConfigureAwait(false);
        }

        private async Task DeleteAllImposters()
        {
            await _client.DeleteAllImpostersAsync().ConfigureAwait(false);
        }

        private void VerifyRequestProxiedToSource()
        {
            _retrievedImposter.Should().NotBeNull("Expected imposter to have been retrieved");
            _retrievedImposter.NumberOfRequests.Should().Be(1);
        }

        private async Task CreateSourceImposter()
        {
            var imposter = _client.CreateTcpImposter(SourceImposterPort);
            imposter.AddStub().ReturnsData("abc123");
            await _client.SubmitAsync(imposter).ConfigureAwait(false);
        }

        private async Task CreateProxyImposter()
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

            await _client.SubmitAsync(proxyImposter).ConfigureAwait(false);
        }

        private async Task MakeRequestToImposter()
        {
            using (var client = new TcpClient("localhost", ProxyImposterPort))
            {
                var data = Encoding.ASCII.GetBytes("testdata");
                using (var stream = client.GetStream())
                {
                    await stream.WriteAsync(data, 0, data.Length).ConfigureAwait(false);
                }
            }
        }
    }
}
