using FluentAssertions;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanCreateImposter
    {
        private readonly MountebankClient _client;
        const int ImposterPort = 6000;

        public CanCreateImposter(MountebankClient client)
        {
            _client = client;
            
        }

        public void Run()
        {
            DeleteAllImposters();
            CreateImposter();
            VerifyImposterHasBeenCreated();
            DeleteAllImposters();
        }

        private void DeleteAllImposters()
        {
            _client.DeleteAllImposters();
        }

        private void VerifyImposterHasBeenCreated()
        {
            var imposter = _client.GetHttpImposter(ImposterPort);
            imposter.Should().NotBeNull();
        }

        private void CreateImposter()
        {
            var imposter = _client.CreateHttpImposter(ImposterPort);
            _client.Submit(imposter);
        }
    }
}