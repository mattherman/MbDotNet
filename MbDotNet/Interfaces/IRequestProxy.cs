using MbDotNet.Models.Imposters;
using System.Threading.Tasks;

namespace MbDotNet.Interfaces
{
    internal interface IRequestProxy
    {
        Task DeleteAllImpostersAsync();
        Task DeleteImposterAsync(int port);
        Task CreateImposterAsync(Imposter imposter);
    }
}
