using FluentAssertions;
using MbDotNet.Models.Imposters;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanCreateAndGetHttpImposter : AcceptanceTest
    {
        private const int ImposterPort = 6000;
        private RetrievedHttpImposter _retrievedImposter;

        public override void Run()
        {
            DeleteAllImposters();
            CreateImposter();
            GetImposter();
            VerifyImposterWasRetrieved();
            DeleteAllImposters();
        }

        private void GetImposter()
        {
            _retrievedImposter = _client.GetHttpImposterAsync(ImposterPort);
        }

        private void DeleteAllImposters()
        {
            _client.DeleteAllImpostersAsync();
        }

        private void VerifyImposterWasRetrieved()
        {
            _retrievedImposter.Should().NotBeNull("Expected imposter to have been retrieved");
        }

        private void CreateImposter()
        {
            var imposter = _client.CreateHttpImposter(ImposterPort);
            _client.SubmitAsync(imposter);
        }
    }
}
