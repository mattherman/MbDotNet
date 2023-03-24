using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// An imposter using the HTTPS protocol
	/// </summary>
	public class HttpsImposter : Imposter, IWithStubs<HttpStub>, IWithResponseFields<HttpResponseFields>
	{
		/// <inheritdoc />
		[JsonProperty("stubs")]
		public ICollection<HttpStub> Stubs { get; private set; }

		private static bool IsPEMFormatted(string value)
			=> Regex.IsMatch(value, @"-----BEGIN CERTIFICATE-----[\S\s]*-----END CERTIFICATE-----");

		private string _cert;
		private string _key;

		/// <summary>
		/// An optional SSL certificate used by the imposter
		/// </summary>
		[JsonProperty("cert", NullValueHandling = NullValueHandling.Ignore)]
		public string Cert
		{
			get => _cert;
			set
			{
				if (value != null && !IsPEMFormatted(value))
				{
					throw new InvalidOperationException("Provided key must be PEM-formatted");
				}

				_cert = value;
			}
		}

		/// <summary>
		/// An optional SSL private key used by the imposter
		/// </summary>
		[JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
		public string Key
		{
			get => _key;
			set
			{
				if (value != null && !IsPEMFormatted(value))
				{
					throw new InvalidOperationException("Provided certificate must be PEM-formatted");
				}

				_key = value;
			}
		}

		/// <summary>
		/// The server will request a client certificate if enabled
		/// </summary>
		[JsonProperty("mutualAuth")]
		public bool MutualAuthRequired { get; set; }

		/// <inheritdoc />
		[JsonProperty("defaultResponse", NullValueHandling = NullValueHandling.Ignore)]
		public HttpResponseFields DefaultResponse { get; set; }

		/// <summary>
		/// Enables CORS requests when set to true, false by default
		/// </summary>
		[JsonProperty("allowCORS")]
		public bool AllowCORS { get; set; }

		/// <summary>
		/// Create a new HttpsImposter instance
		/// </summary>
		/// <param name="port">An optional port for the imposter to listen on</param>
		/// <param name="name">An optional name for the imposter</param>
		/// <param name="recordRequests">Whether or not Mountebank should record requests made to the imposter, defaults to false</param>
		/// <param name="defaultResponse">An optional default response for when no predicates match a request</param>
		/// <param name="allowCORS">Enables CORS requests, defaults to false</param>
		public HttpsImposter(int? port, string name, bool recordRequests = false, HttpResponseFields defaultResponse = null, bool allowCORS = false)
			: this(port, name, null, null, false, recordRequests, defaultResponse, allowCORS)
		{
		}

		/// <summary>
		/// Create a new HttpsImposter instance
		/// </summary>
		/// <param name="port">An optional port for the imposter to listen on</param>
		/// <param name="name">An optional name for the imposter</param>
		/// <param name="mutualAuthRequired">Forces the server to request a client certificate, defaults to false</param>
		/// <param name="recordRequests">Whether or not Mountebank should record requests made to the imposter, defaults to false</param>
		/// <param name="defaultResponse">An optional default response for when no predicates match a request</param>
		/// <param name="allowCORS">Enables CORS requests, defaults to false</param>
		/// <param name="key">An optional SSL private key used by the imposter</param>
		/// <param name="cert">An optional SSL certificate used by the imposter</param>
		public HttpsImposter(int? port, string name, string key, string cert, bool mutualAuthRequired,
			bool recordRequests = false, HttpResponseFields defaultResponse = null, bool allowCORS = false) : base(port, Enums.Protocol.Https, name, recordRequests)
		{
			Cert = cert;
			Key = key;
			MutualAuthRequired = mutualAuthRequired;
			Stubs = new List<HttpStub>();
			DefaultResponse = defaultResponse;
			AllowCORS = allowCORS;
		}

		/// <inheritdoc />
		public HttpStub AddStub()
		{
			var stub = new HttpStub();
			Stubs.Add(stub);
			return stub;
		}
	}
}
