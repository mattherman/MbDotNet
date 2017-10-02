using FluentAssertions;
using MbDotNet.Models.Imposters;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanCreateAndGetTcpImposter : AcceptanceTest
    {
        private const int ImposterPort = 6000;
        private RetrievedTcpImposter _retrievedImposter;

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
            _client.DeleteAllImposters();
        }

        private void GetImposter()
        {
            _retrievedImposter = _client.GetTcpImposter(ImposterPort);
        }

        private void VerifyImposterWasRetrieved()
        {
            _retrievedImposter.Should().NotBeNull("Expected imposter to have been retrieved");
        }

        private void CreateImposter()
        {
            var imposter = _client.CreateTcpImposter(ImposterPort);
            _client.Submit(imposter);
        }
    }
}
