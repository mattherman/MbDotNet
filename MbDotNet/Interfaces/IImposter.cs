using MbDotNet.Enums;

namespace MbDotNet.Interfaces
{
    public interface IImposter
    {
        int Port { get; }
        string Protocol { get; }
        bool PendingSubmission { get; set; }

        Stub AddStub();
    }
}
