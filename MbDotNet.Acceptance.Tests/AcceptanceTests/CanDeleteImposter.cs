using FluentAssertions;
using MbDotNet.Exceptions;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanDeleteImposter
    {
        private readonly MountebankClient _client;
        const int ImposterPort = 6001;

        public CanDeleteImposter(MountebankClient client)
        {
            _client = client;
        }

        public void Run()
        {
            DeleteImposter();
            CreateImposter();
            DeleteImposter();
            VerifyImposterHasBeenDeleted();

            DeleteAllImposters();
        }

        private void DeleteAllImposters()
        {
            _client.DeleteAllImposters();
        }

        private void DeleteImposter()
        {
            _client.DeleteImposter(ImposterPort);
        }

        private void VerifyImposterHasBeenDeleted()
        {
            MountebankException exception = null;
            try
            {
                var imposter = _client.GetImposter(ImposterPort);
            }
            catch (ImposterNotFoundException e)
            {
                exception = e;

            }

            exception.Should().NotBeNull("Expected imposter to have been deleted");
            exception.Message.Should().Contain("no such resource");
        }

        private void CreateImposter()
        {
            var imposter = _client.CreateHttpImposter(ImposterPort);
            _client.Submit(imposter);
        }
    }
}