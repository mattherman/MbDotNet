using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MbDotNet.Models.Requests
{
	/// <summary>
	/// Content of an email attachment
	/// </summary>
	public class EmailContent
	{
		/// <summary>
		/// The type of the content
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type { get; internal set; }

		/// <summary>
		/// The binary data of the content
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("data")]
		[JsonConverter(typeof(ByteArrayFromNumberArrayConverter))]
		public byte[] Data { get; internal set; }
	}

	internal class ByteArrayFromNumberArrayConverter : JsonConverter<byte[]>
	{
		public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.StartArray)
			{
				var bytes = new List<byte>();
				while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
				{
					bytes.Add(reader.GetByte());
				}
				return bytes.ToArray();
			}

			// Fall back to base64 string
			return reader.GetBytesFromBase64();
		}

		public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
		{
			writer.WriteBase64StringValue(value);
		}
	}
}
