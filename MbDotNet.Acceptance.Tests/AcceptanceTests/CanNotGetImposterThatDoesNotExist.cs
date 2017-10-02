using System;
using FluentAssertions;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanNotGetImposterThatDoesNotExist : AcceptanceTest
    {
        private RetrievedHttpImposter _retrievedImposter;
        private Exception _getImposterException;
        private const int NonExistentImposterPort = 9000;

        public override void Run()
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
                _retrievedImposter = _client.GetHttpImposter(NonExistentImposterPort);
            }
            catch (ImposterNotFoundException e)
            {
                _getImposterException = e;
            }
        }

    }
}
