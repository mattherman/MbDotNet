using FluentAssertions;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;

namespace MbDotNet.Acceptance.Tests
{
    internal class CanGetImposter
    {
        private readonly MountebankClient _client;
        const int _imposterPort = 6000;
        private RetrievedImposter _retrievedImposter;

        public CanGetImposter(MountebankClient client)
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
            _retrievedImposter = _client.GetImposter(_imposterPort);
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
            var imposter = _client.CreateHttpImposter(_imposterPort);
            _client.Submit(imposter);
        }
    }
}