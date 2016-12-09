using System;
using FluentAssertions;
using MbDotNet.Models.Imposters;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanNotGetImposterThatDoesNotExist
    {
        private readonly MountebankClient _client;
        private RetrievedImposter _retrievedImposter;
        private Exception _getImposterException;
        const int NonExistentImposterPort = 9000;

        public CanNotGetImposterThatDoesNotExist(MountebankClient client)
        {
            _client = client;
        }

        public void Run()
        {
            GetNonExistentImposter();

            VerifyNoImposterWasRetrieved();
            VerifyExceptionForNoSuchResourceWasThrown();
        }

        private void VerifyExceptionForNoSuchResourceWasThrown()
        {
            _getImposterException.Should().NotBeNull();
            _getImposterException.Message.Should().Contain("no such resource");
        }

        private void VerifyNoImposterWasRetrieved()
        {
            _retrievedImposter.Should().BeNull();
        }

        private void GetNonExistentImposter()
        {
            try
            {
                _retrievedImposter = _client.GetImposter(NonExistentImposterPort);
            }
            catch (Exception e)
            {
                _getImposterException = e;
            }
        }

    }
}