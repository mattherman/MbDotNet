using MbDotNet.Enums;

namespace MbDotNet.Interfaces
{
    public interface IImposter
    {
        int Port { get; }
        string Protocol { get; }
        bool PendingSubmission { get; }

        Stub AddStub();
        void Submit();
        void Delete();
    }
}
