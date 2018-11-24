using FluentAssertions;
using MbDotNet.Models.Imposters;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanCreateAndGetHttpsImposter : AcceptanceTest
    {
        private const int ImposterPort = 6000;
        private RetrievedHttpsImposter _retrievedImposter;

        public override void Run()
        {
            DeleteAllImposters();
            CreateImposter();
            GetImposter();
            VerifyImposterWasRetrieved();
            DeleteAllImposters();
        }

        private void DeleteAllImposters()
        {
            _client.DeleteAllImpostersAsync();
        }

        private void GetImposter()
        {
            _retrievedImposter = _client.GetHttpsImposterAsync(ImposterPort);
        }

        private void VerifyImposterWasRetrieved()
        {
            _retrievedImposter.Should().NotBeNull("Expected imposter to have been retrieved");
        }

        private void CreateImposter()
        {
            var imposter = _client.CreateHttpsImposter(ImposterPort);
            _client.SubmitAsync(imposter);
        }
    }
}
