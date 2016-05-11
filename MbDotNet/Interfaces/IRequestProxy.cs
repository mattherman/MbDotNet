namespace MbDotNet.Interfaces
{
    internal interface IRequestProxy
    {
        void DeleteAllImposters();
        void DeleteImposter(int port);
        void CreateImposter(IImposter imposter);
    }
}
