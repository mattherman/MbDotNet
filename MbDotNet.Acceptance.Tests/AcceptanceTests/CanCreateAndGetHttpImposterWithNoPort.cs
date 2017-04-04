using FluentAssertions;
using MbDotNet.Models.Imposters;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanCreateAndGetHttpImposterWithNoPort
    {
        private readonly MountebankClient _client;
        private Imposter _imposter = null;
        private RetrievedHttpImposter _retrievedImposter;

        public CanCreateAndGetHttpImposterWithNoPort(MountebankClient client)
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
            _retrievedImposter = _client.GetHttpImposter(_imposter.Port.Value);
        }

        private void DeleteAllImposters()
        {
            _client.DeleteAllImposters();
        }

        private void VerifyImposterWasRetrieved()
        {
            _retrievedImposter.Should().NotBeNull("Expected imposter to have been retrieved");
            _retrievedImposter.Port.Should().Equals(_imposter.Port);
        }

        private void CreateImposter()
        {
            _imposter = _client.CreateHttpImposter();
            _client.Submit(_imposter);
        }
    }
}