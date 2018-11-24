using FluentAssertions;
using MbDotNet.Exceptions;
using System.Threading.Tasks;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanDeleteImposter : AcceptanceTest
    {
        private const int ImposterPort = 6001;

        public override async Task Run()
        {
            await DeleteImposter().ConfigureAwait(false);
            await CreateImposter().ConfigureAwait(false);
            await DeleteImposter().ConfigureAwait(false);
            await VerifyImposterHasBeenDeleted().ConfigureAwait(false);
            
            await DeleteAllImposters().ConfigureAwait(false);
        }

        private async Task DeleteAllImposters()
        {
            await _client.DeleteAllImpostersAsync().ConfigureAwait(false);
        }

        private async Task DeleteImposter()
        {
            await _client.DeleteImposterAsync(ImposterPort).ConfigureAwait(false);
        }

        private async Task VerifyImposterHasBeenDeleted()
        {
            MountebankException exception = null;
            try
            {
                await _client.GetHttpImposterAsync(ImposterPort).ConfigureAwait(false);
            }
            catch (ImposterNotFoundException e)
            {
                exception = e;

            }

            exception.Should().NotBeNull("Expected imposter to have been deleted");
            exception.Message.Should().Contain("no such resource");
        }

        private async Task CreateImposter()
        {
            var imposter = _client.CreateHttpImposter(ImposterPort);
            await _client.SubmitAsync(imposter).ConfigureAwait(false);
        }
    }
}
