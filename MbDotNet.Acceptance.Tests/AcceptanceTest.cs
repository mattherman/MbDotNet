
using System;
using System.Net;
using FluentAssertions;
using MbDotNet.Exceptions;

namespace MbDotNet.Acceptance.Tests
{
    internal class AcceptanceTest
    {
        private readonly MountebankClient _client;

        public AcceptanceTest(MountebankClient client)
        {
            _client = client;
        }

        public void CanCreateImposter()
        {
            DeleteAllImposters();
            CreateImposter();
            VerifyImposterHasBeenCreated();
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
                var imposter = _client.CreateHttpImposter(6000);
                imposter.AddStub().ReturnsStatus(HttpStatusCode.BadRequest);
                _client.Submit();
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
            var imposter = _client.CreateHttpImposter(6000);
            imposter.AddStub().ReturnsStatus(HttpStatusCode.BadRequest);
            _client.Submit();
        }
    }
}