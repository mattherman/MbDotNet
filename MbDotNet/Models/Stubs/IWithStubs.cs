using System.Collections.Generic;

namespace MbDotNet.Models.Stubs
{
	/// <summary>
	/// Represents a type that accepts and exposes stubs
	/// </summary>
	/// <typeparam name="TStub"></typeparam>
	public interface IWithStubs<TStub> where TStub : Stub
	{
		/// <summary>
		/// The configured stubs
		/// </summary>
		ICollection<TStub> Stubs { get; }

		/// <summary>
		/// Adds an empty stub
		/// </summary>
		/// <returns>The new stub</returns>
		TStub AddStub();
	}
}
