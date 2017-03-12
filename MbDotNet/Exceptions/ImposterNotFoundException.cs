using System;

namespace MbDotNet.Exceptions
{
    /// <summary>
    /// Represents errors caused by a missing imposter.
    /// </summary>
    [Serializable]
    public class ImposterNotFoundException : MountebankException
    {
        public ImposterNotFoundException(string message) : base (message) { }
        public ImposterNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}