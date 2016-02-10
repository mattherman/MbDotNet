using System;

namespace MbDotNet.Exceptions
{
    [Serializable]
    public class MountebankException : Exception
    {
        public MountebankException(string message) : base(message) { }

        public void Test()
        {
            var obj = new { Key = "x", Value = "y" };
        }
    }
}
