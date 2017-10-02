using FluentAssertions;
using MbDotNet.Models.Imposters;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanCreateAndGetHttpImposterWithNoPort : AcceptanceTest
    {
        private Imposter _imposter = null;
        private RetrievedHttpImposter _retrievedImposter;

        public override void Run()
        {
            DeleteAllImposters();
            CreateImposter();
            GetImposter();
            VerifyImposterWasRetrieved();
            DeleteAllImposters();
        }

        private void GetImposter()
        {
            _retrievedImposter = _client.GetHttpImposter(_imposter.Port);
        }

        private void DeleteAllImposters()
        {
            _client.DeleteAllImposters();
        }

        private void VerifyImposterWasRetrieved()
        {
            _retrievedImposter.Should().NotBeNull("Expected imposter to have been retrieved");
            _retrievedImposter.Port.Should().Equals(_imposter.Port);
        }

        private void CreateImposter()
        {
            _imposter = _client.CreateHttpImposter();
            _client.Submit(_imposter);
        }
    }
}
