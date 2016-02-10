using System;

namespace MbDotNet.Exceptions
{
    [Serializable]
    public class MountebankException : Exception
    {
        public MountebankException(string message) : base(message) { }
    }
}
