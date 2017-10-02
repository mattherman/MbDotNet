using FluentAssertions;
using MbDotNet.Exceptions;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanDeleteImposter : AcceptanceTest
    {
        private const int ImposterPort = 6001;

        public override void Run()
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
                _client.GetHttpImposter(ImposterPort);
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
