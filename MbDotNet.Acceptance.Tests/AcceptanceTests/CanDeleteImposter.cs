using System.Net;
using FluentAssertions;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;

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
                var imposter = _client.CreateHttpImposter(ImposterPort);
                _client.Submit(imposter);
            }
            catch (MountebankException e)
            {
                exception = e;

            }

            exception.Should().BeNull("Expected imposter to have been deleted");
        }

        private void CreateImposter()
        {
            var imposter = _client.CreateHttpImposter(ImposterPort);
            _client.Submit(imposter);
        }
    }
}