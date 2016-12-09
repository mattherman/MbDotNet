using System;

namespace MbDotNet.Exceptions
{
    [Serializable]
    public class ImposterNotFoundException : MountebankException
    {
        public ImposterNotFoundException(string message) : base (message) { }
        public ImposterNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}