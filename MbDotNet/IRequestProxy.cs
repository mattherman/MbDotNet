using MbDotNet.Models.Imposters;
using System.Threading.Tasks;

namespace MbDotNet
{
    internal interface IRequestProxy
    {
        Task DeleteAllImpostersAsync();
        Task DeleteImposterAsync(int port);
        Task CreateImposterAsync(Imposter imposter);
        Task<RetrievedHttpImposter> GetHttpImposterAsync(int port);
        Task<RetrievedTcpImposter> GetTcpImposterAsync(int port);
        Task<RetrievedHttpsImposter> GetHttpsImposterAsync(int port);
    }
}
