using MbDotNet.Enums;
using MbDotNet.Exceptions;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// Abstract representation of an imposter. All protocol-specific imposters derive from this type.
	/// </summary>
	public abstract class Imposter
	{
		/// <summary>
		/// The port the imposter is set up to accept requests on.
		/// </summary>
		[JsonProperty(PropertyName = "port", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int Port { get; private set; }

		/// <summary>
		/// The protocol the imposter is set up to accept requests through.
		/// </summary>
		[JsonProperty("protocol")]
		public string Protocol { get; private set; }

		/// <summary>
		/// Optional name for the imposter, used in the logs.
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; private set; }

		/// <summary>
		/// Enables recording requests to use the imposter as a mock. See <see href="http://www.mbtest.org/docs/api/mocks">here</see> for more details on Mountebank verification.
		/// </summary>
		[JsonProperty("recordRequests")]
		public bool RecordRequests { get; private set; }

		// TODO: Remove this and add a body to a setter
		internal void SetDynamicPort(int port)
		{
			if (Port != default)
			{
				throw new MountebankException("Cannot change imposter port once it has been set.");
			}

			Port = port;
		}

		/// <summary>
		/// Create a new Imposter instance
		/// </summary>
		/// <param name="port">An optional port the imposter should be associated with</param>
		/// <param name="protocol">The network protocol of the imposter</param>
		/// <param name="name">An optional name for the imposter</param>
		/// <param name="recordRequests">Whether or not Mountebank should record requests made to the imposter</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Set as virtual for testing purposes")]
		protected Imposter(int? port, Protocol protocol, string name, bool recordRequests)
		{
			if (port.HasValue)
			{
				Port = port.Value;
			}

			Protocol = protocol.ToString().ToLower();
			Name = name;
			RecordRequests = recordRequests;
		}
	}
}
