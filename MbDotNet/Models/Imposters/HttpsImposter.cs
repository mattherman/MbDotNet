using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;
using System.Text.Json.Serialization;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// An imposter using the HTTPS protocol
	/// </summary>
	public class HttpsImposter : Imposter, IWithStubs<HttpStub>, IWithResponseFields<HttpResponseFields>
	{
		/// <inheritdoc />
		[JsonInclude]
		[JsonPropertyName("stubs")]
		public IList<HttpStub> Stubs { get; private set; }

		private static bool IsPEMFormatted(string value)
			=> Regex.IsMatch(value, @"-----BEGIN CERTIFICATE-----[\S\s]*-----END CERTIFICATE-----");

		private string _cert;
		private string _key;

		/// <summary>
		/// An optional SSL certificate used by the imposter
		/// </summary>
		[JsonPropertyName("cert")]
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
		[JsonPropertyName("key")]
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
		[JsonPropertyName("mutualAuth")]
		public bool MutualAuthRequired { get; set; }

		/// <inheritdoc />
		[JsonPropertyName("defaultResponse")]
		public HttpResponseFields DefaultResponse { get; set; }

		/// <summary>
		/// Enables CORS requests when set to true, false by default
		/// </summary>
		[JsonPropertyName("allowCORS")]
		public bool AllowCORS { get; set; }

		/// <summary>
		/// Create a new HttpsImposter instance
		/// </summary>
		/// <param name="port">An optional port for the imposter to listen on</param>
		/// <param name="name">An optional name for the imposter</param>
		/// <param name="options">Options for configuring the imposter</param>
		public HttpsImposter(int? port, string name, HttpsImposterOptions options) : base(port, Imposters.Protocol.Https, name, options?.RecordRequests ?? false)
		{
			Cert = options?.Cert;
			Key = options?.Key;
			MutualAuthRequired = options?.MutualAuthRequired ?? false;
			DefaultResponse = options?.DefaultResponse;
			AllowCORS = options?.AllowCORS ?? false;
			Stubs = new List<HttpStub>();
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
