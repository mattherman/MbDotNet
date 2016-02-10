using System.Collections.Generic;
using MbDotNet.Enums;

namespace MbDotNet.Interfaces
{
    public interface IClient
    {
        ICollection<IImposter> Imposters { get; }

        IImposter CreateImposter(int port, Protocol protocol);
        void DeleteImposter(int port);
        void DeleteAllImposters();
        void Submit();
    }
}
