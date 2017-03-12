using FluentAssertions;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    public class CanCreateAndGetTcpImposter
    {
        private readonly MountebankClient _client;
        const int ImposterPort = 6000;

        public CanCreateAndGetTcpImposter(MountebankClient client)
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
            var imposter = _client.GetTcpImposter(ImposterPort);
            imposter.Should().NotBeNull();
        }

        private void CreateImposter()
        {
            var imposter = _client.CreateTcpImposter(ImposterPort);
            _client.Submit(imposter);
        }
    }
}
