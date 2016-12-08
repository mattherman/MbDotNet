using System.Net;
using FluentAssertions;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanDeleteImposter
    {
        private readonly MountebankClient _client;
        private int _imposterPort = 6001;

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
            _client.DeleteImposter(_imposterPort);
        }

        private void VerifyImposterHasBeenDeleted()
        {
            MountebankException exception = null;
            try
            {
                var imposter = _client.CreateHttpImposter(_imposterPort);
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
            var imposter = _client.CreateHttpImposter(_imposterPort);
            _client.Submit(imposter);
        }
    }
}