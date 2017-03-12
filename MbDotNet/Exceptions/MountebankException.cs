using System;

namespace MbDotNet.Exceptions
{
    /// <summary>
    /// Represents errors caused when interactions with mountebank fail.
    /// </summary>
    [Serializable]
    public class MountebankException : Exception
    {
        public MountebankException(string message) : base(message) { }
        public MountebankException(string message, Exception innerException) : base(message, innerException) { }
    }
}
