using FluentAssertions;
using MbDotNet.Models.Imposters;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanCreateAndGetHttpImposter
    {
        private readonly MountebankClient _client;
        private const int ImposterPort = 6000;
        private RetrievedHttpImposter _retrievedImposter;

        public CanCreateAndGetHttpImposter(MountebankClient client)
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

        private void GetImposter()
        {
            _retrievedImposter = _client.GetHttpImposter(ImposterPort);
        }

        private void DeleteAllImposters()
        {
            _client.DeleteAllImposters();
        }

        private void VerifyImposterWasRetrieved()
        {
            _retrievedImposter.Should().NotBeNull("Expected imposter to have been retrieved");
        }

        private void CreateImposter()
        {
            var imposter = _client.CreateHttpImposter(ImposterPort);
            _client.Submit(imposter);
        }
    }
}