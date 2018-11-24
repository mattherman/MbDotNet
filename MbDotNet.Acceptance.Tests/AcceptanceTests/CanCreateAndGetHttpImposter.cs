using FluentAssertions;
using MbDotNet.Models.Imposters;
using System.Threading.Tasks;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanCreateAndGetHttpImposter : AcceptanceTest
    {
        private const int ImposterPort = 6000;
        private RetrievedHttpImposter _retrievedImposter;

        public override async Task Run()
        {
            await DeleteAllImposters().ConfigureAwait(false);
            await CreateImposter().ConfigureAwait(false);
            await GetImposter().ConfigureAwait(false);
            VerifyImposterWasRetrieved();
            await DeleteAllImposters().ConfigureAwait(false);
        }

        private async Task GetImposter()
        {
            _retrievedImposter = await _client.GetHttpImposterAsync(ImposterPort).ConfigureAwait(false);
        }

        private async Task DeleteAllImposters()
        {
            await _client.DeleteAllImpostersAsync().ConfigureAwait(false);
        }

        private void VerifyImposterWasRetrieved()
        {
            _retrievedImposter.Should().NotBeNull("Expected imposter to have been retrieved");
        }

        private async Task CreateImposter()
        {
            var imposter = _client.CreateHttpImposter(ImposterPort);
            await _client.SubmitAsync(imposter).ConfigureAwait(false);
        }
    }
}
