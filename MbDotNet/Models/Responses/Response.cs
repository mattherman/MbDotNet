using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MbDotNet.Models.Responses
{
	/// <summary>
	/// An abstract representation of a response without a specific protocol
	/// </summary>
	public abstract class Response
	{
		/// <summary>
		/// Configured response behaviors
		/// </summary>
		[JsonProperty("behaviors", NullValueHandling = NullValueHandling.Ignore)]
		public IList<Behavior> Behaviors { get; set; }

		/// <summary>
		/// Construct a new Response instance
		/// </summary>
		/// <param name="responseBehaviors">Response behaviors to include on the response</param>
		protected Response(IEnumerable<Behavior> responseBehaviors)
		{
			Behaviors = responseBehaviors?.ToList();
		}
	}
}
