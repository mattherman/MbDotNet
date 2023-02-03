using System;

namespace MbDotNet.Exceptions
{
	/// <summary>
	/// Represents errors caused by a missing imposter.
	/// </summary>
	[Serializable]
	public class ImposterNotFoundException : MountebankException
	{
		/// <inheritdoc />
		public ImposterNotFoundException(string message) : base(message) { }

		/// <inheritdoc />
		public ImposterNotFoundException(string message, Exception innerException) : base(message, innerException) { }
	}
}
