using System;
using System.Threading.Tasks;
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

        public override async Task Run()
        {
            await GetNonExistentImposter().ConfigureAwait(false);

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

        private async Task GetNonExistentImposter()
        {
            try
            {
                _retrievedImposter = await _client.GetHttpImposterAsync(NonExistentImposterPort).ConfigureAwait(false);
            }
            catch (ImposterNotFoundException e)
            {
                _getImposterException = e;
            }
        }

    }
}
