using FluentAssertions;
using MbDotNet.Models.Imposters;
using System.Threading.Tasks;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanCreateAndGetHttpImposterWithNoPort : AcceptanceTest
    {
        private Imposter _imposter = null;
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
            _retrievedImposter = await _client.GetHttpImposterAsync(_imposter.Port).ConfigureAwait(false);
        }

        private async Task DeleteAllImposters()
        {
            await _client.DeleteAllImpostersAsync().ConfigureAwait(false);
        }

        private void VerifyImposterWasRetrieved()
        {
            _retrievedImposter.Should().NotBeNull("Expected imposter to have been retrieved");
            _retrievedImposter.Port.Should().Equals(_imposter.Port);
        }

        private async Task CreateImposter()
        {
            _imposter = _client.CreateHttpImposter();
            await _client.SubmitAsync(_imposter).ConfigureAwait(false);
        }
    }
}
