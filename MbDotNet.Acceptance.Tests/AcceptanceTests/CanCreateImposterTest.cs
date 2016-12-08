using System.Net;
using FluentAssertions;
using MbDotNet.Exceptions;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanCreateImposterTest
    {
        private readonly MountebankClient _client;
        private int imposterPort = 6000;

        public CanCreateImposterTest(MountebankClient client)
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
            MountebankException exception = null;
            try
            {
                var imposter = _client.CreateHttpImposter(imposterPort);
                imposter.AddStub().ReturnsStatus(HttpStatusCode.BadRequest);
                _client.Submit(imposter);
            }
            catch (MountebankException e)
            {
                exception = e;

            }

            exception.Should().NotBeNull("Expected imposter to already exist on port");
            exception.Message.Should().Contain("port is already in use", "Expected imposter to already exist on port");
        }

        private void CreateImposter()
        {
            var imposter = _client.CreateHttpImposter(imposterPort);
            _client.Submit(imposter);
        }
    }
}