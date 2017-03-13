using FluentAssertions;
using MbDotNet.Models.Imposters;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanCreateAndGetHttpsImposter
    {
        private readonly MountebankClient _client;
        private const int ImposterPort = 6000;
        private RetrievedHttpsImposter _retrievedImposter;

        public CanCreateAndGetHttpsImposter(MountebankClient client)
        {
            _client = client;

        }

        public void Run()
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
            _retrievedImposter = _client.GetHttpsImposter(ImposterPort);
        }

        private void VerifyImposterWasRetrieved()
        {
            _retrievedImposter.Should().NotBeNull("Expected imposter to have been retrieved");
        }

        private void CreateImposter()
        {
            var imposter = _client.CreateHttpsImposter(ImposterPort);
            _client.Submit(imposter);
        }
    }
}