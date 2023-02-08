using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace MbDotNet.Models.Responses.Fields
{
	/// <summary>
	/// Response fields that can be set for HTTP requests
	/// </summary>
	public class HttpResponseFields : ResponseFields
	{
		/// <summary>
		/// The HTTP status code of the response
		/// </summary>
		[JsonProperty("statusCode", NullValueHandling = NullValueHandling.Ignore)]
		public HttpStatusCode? StatusCode { get; set; }

		/// <summary>
		/// The body of the response
		/// </summary>
		[JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
		public object ResponseObject { get; set; }

		/// <summary>
		/// The HTTP headers
		/// </summary>
		[JsonProperty("headers", NullValueHandling = NullValueHandling.Ignore)]
		public IDictionary<string, object> Headers { get; set; }

		/// <summary>
		/// The mode of the response, "text" (default) or "binary"
		/// </summary>
		[JsonProperty("_mode", NullValueHandling = NullValueHandling.Ignore)]
		public string Mode { get; set; }
	}
}
