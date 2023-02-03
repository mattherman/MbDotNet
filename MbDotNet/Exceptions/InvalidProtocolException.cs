using System;

namespace MbDotNet.Exceptions
{
	/// <summary>
	/// Represents errors caused either by an invalid or unexpected protocol type.
	/// </summary>
	[Serializable]
	public class InvalidProtocolException : MountebankException
	{
		/// <inheritdoc />
		public InvalidProtocolException(string message) : base(message) { }

		/// <inheritdoc />
		public InvalidProtocolException(string message, Exception innerException) : base(message, innerException) { }
	}
}
