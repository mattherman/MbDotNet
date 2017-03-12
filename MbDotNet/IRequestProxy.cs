using MbDotNet.Models.Imposters;

namespace MbDotNet
{
    internal interface IRequestProxy
    {
        void DeleteAllImposters();
        void DeleteImposter(int port);
        void CreateImposter(Imposter imposter);
        RetrievedImposter GetImposter(int port);
    }
}
