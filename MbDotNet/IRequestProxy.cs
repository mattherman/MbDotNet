using MbDotNet.Models.Imposters;

namespace MbDotNet
{
    internal interface IRequestProxy
    {
        void DeleteAllImposters();
        void DeleteImposter(int port);
        void CreateImposter(Imposter imposter);
        RetrievedHttpImposter GetHttpImposter(int port);
        RetrievedTcpImposter GetTcpImposter(int port);
        RetrievedHttpsImposter GetHttpsImposter(int port);
        void DeleteSavedRequests(int port);
    }
}
