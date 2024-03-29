using MbDotNet.Exceptions;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// Abstract representation of an imposter. All protocol-specific imposters derive from this type.
	/// </summary>
	public abstract class Imposter
	{
		private int _port;

		/// <summary>
		/// The port the imposter is set up to accept requests on.
		/// </summary>
		[JsonProperty(PropertyName = "port", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int Port
		{
			get => _port;
			internal set
			{
				if (_port != default)
				{
					throw new MountebankException("Cannot change imposter port once it has been set.");
				}

				_port = value;
			}
		}

		/// <summary>
		/// The protocol the imposter is set up to accept requests through.
		/// </summary>
		[JsonProperty("protocol")]
		public string Protocol { get; private set; }

		/// <summary>
		/// Optional name for the imposter, used in the logs.
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// Enables recording requests to use the imposter as a mock. See <see href="http://www.mbtest.org/docs/api/mocks">here</see> for more details on Mountebank verification.
		/// </summary>
		[JsonProperty("recordRequests")]
		public bool RecordRequests { get; set; }

		/// <summary>
		/// Create a new Imposter instance
		/// </summary>
		/// <param name="port">An optional port the imposter should be associated with</param>
		/// <param name="protocol">The network protocol of the imposter</param>
		/// <param name="name">An optional name for the imposter</param>
		/// <param name="recordRequests">Whether or not Mountebank should record requests made to the imposter</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Set as virtual for testing purposes")]
		protected Imposter(int? port, Protocol protocol, string name, bool recordRequests) : this(port, protocol.ToString().ToLower(), name, recordRequests)
		{
		}

		/// <summary>
		/// Create a new Imposter instance. Supports plain string protocol names to allow custom protocols.
		/// </summary>
		/// <param name="port">An optional port the imposter should be associated with</param>
		/// <param name="protocol">The network protocol of the imposter</param>
		/// <param name="name">An optional name for the imposter</param>
		/// <param name="recordRequests">Whether or not Mountebank should record requests made to the imposter</param>
		protected Imposter(int? port, string protocol, string name, bool recordRequests)
		{
			if (port.HasValue)
			{
				Port = port.Value;
			}

			Protocol = protocol.ToLower();
			Name = name;
			RecordRequests = recordRequests;
		}
	}
}
