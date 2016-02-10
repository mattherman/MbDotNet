namespace MbDotNet.Interfaces
{
    public interface IRequestProxy
    {
        void DeleteAllImposters();
        void DeleteImposter(int port);
        void CreateImposter(IImposter imposter);
    }
}
