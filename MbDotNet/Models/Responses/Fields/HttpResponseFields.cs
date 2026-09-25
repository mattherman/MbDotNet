using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

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
		[JsonPropertyName("statusCode")]
		[JsonConverter(typeof(HttpStatusCodeConverter))]
		public HttpStatusCode? StatusCode { get; set; }

		/// <summary>
		/// The body of the response
		/// </summary>
		[JsonPropertyName("body")]
		[JsonConverter(typeof(JsonHelper.ConsumerPayloadConverter))]
		public object ResponseObject { get; set; }

		/// <summary>
		/// The HTTP headers
		/// </summary>
		[JsonPropertyName("headers")]
		public IDictionary<string, object> Headers { get; set; }

		/// <summary>
		/// The mode of the response, "text" (default) or "binary"
		/// </summary>
		[JsonPropertyName("_mode")]
		public string Mode { get; set; }
	}

	internal class HttpStatusCodeConverter : JsonConverter<HttpStatusCode?>
	{
		public override HttpStatusCode? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Null)
				return null;
			return (HttpStatusCode)reader.GetInt32();
		}

		public override void Write(Utf8JsonWriter writer, HttpStatusCode? value, JsonSerializerOptions options)
		{
			if (value == null)
				writer.WriteNullValue();
			else
				writer.WriteNumberValue((int)value);
		}
	}
}
