using MbDotNet.Models.Imposters;

namespace MbDotNet.Interfaces
{
    internal interface IRequestProxy
    {
        void DeleteAllImposters();
        void DeleteImposter(int port);
        void CreateImposter(Imposter imposter);
        RetrievedImposter GetImposter(int port);
    }
}
