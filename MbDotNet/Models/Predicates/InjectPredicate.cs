using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
	/// <summary>
	/// A "inject" predicate
	/// </summary>
	public class InjectPredicate : PredicateBase
	{
		/// <summary>
		/// A javascript function that is run to determine whether to match a stub.
		/// The function should take a single request object that will contain the request, an
		/// empty state object to share with response injectors, and a logger.
		/// </summary>
		[JsonProperty("inject")]
		public string InjectedFunction { get; private set; }

		/// <summary>
		/// Create a new InjectPredicate instance
		/// </summary>
		/// <param name="injectedFunction">A javascript function that is run to determine whether to match a stub</param>
		public InjectPredicate(string injectedFunction)
		{
			InjectedFunction = injectedFunction;
		}
	}
}
