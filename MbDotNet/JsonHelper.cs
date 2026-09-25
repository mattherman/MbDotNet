using System;
using System.Collections;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MbDotNet
{
	internal static class JsonHelper
	{
		/// <summary>
		/// Options used for MbDotNet's own model types. The relaxed encoder is used because payloads
		/// are sent to mountebank's REST API and are never embedded in HTML; the default encoder
		/// would escape characters common in XML bodies and injected JavaScript.
		/// </summary>
		internal static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			IncludeFields = true,
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
			Converters = { new JsonStringEnumConverter(), new PolymorphicConverterFactory() }
		};

		/// <summary>
		/// Options used for caller-supplied payload objects, such as the response body passed to
		/// ReturnsJson or the request body of a predicate. These deliberately mirror Newtonsoft.Json's
		/// defaults so that upgrading does not silently change the shape of a caller's payload:
		/// nulls are written rather than omitted, and enums are written as numbers rather than names.
		/// </summary>
		internal static readonly JsonSerializerOptions PayloadOptions = new JsonSerializerOptions
		{
			IncludeFields = true,
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
			Converters = { new PolymorphicConverterFactory() }
		};

		internal static string Serialize<T>(T value)
		{
			if (value == null) return "null";
			return JsonSerializer.Serialize(value, value.GetType(), SerializerOptions);
		}

		internal static T Deserialize<T>(string json)
		{
			return JsonSerializer.Deserialize<T>(json, SerializerOptions);
		}

		/// <summary>
		/// Serializes a caller-supplied payload object using <see cref="PayloadOptions"/> so that it
		/// keeps Newtonsoft.Json-compatible shape even though the surrounding model uses
		/// <see cref="SerializerOptions"/>. Apply to properties that hold objects provided by the caller.
		/// </summary>
		internal class ConsumerPayloadConverter : JsonConverter<object>
		{
			public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				return JsonElement.ParseValue(ref reader);
			}

			public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
			{
				if (value == null)
				{
					writer.WriteNullValue();
					return;
				}

				JsonSerializer.Serialize(writer, value, value.GetType(), PayloadOptions);
			}
		}

		/// <summary>
		/// Write-only converter factory that serializes abstract/interface types using their
		/// runtime type, enabling polymorphic serialization of Imposter, Predicate, Response, etc.
		/// Excludes collection interfaces which System.Text.Json handles natively.
		/// </summary>
		private class PolymorphicConverterFactory : JsonConverterFactory
		{
			public override bool CanConvert(Type typeToConvert)
			{
				if (!typeToConvert.IsAbstract && !typeToConvert.IsInterface)
					return false;

				// Let STJ handle collection interfaces natively
				if (typeof(IEnumerable).IsAssignableFrom(typeToConvert))
					return false;

				return true;
			}

			public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
			{
				var converterType = typeof(PolymorphicConverter<>).MakeGenericType(typeToConvert);
				return (JsonConverter)Activator.CreateInstance(converterType);
			}
		}

		private class PolymorphicConverter<T> : JsonConverter<T>
		{
			public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				throw new NotSupportedException($"Deserialization of abstract type {typeof(T)} is not supported.");
			}

			public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
			{
				if (value == null)
				{
					writer.WriteNullValue();
					return;
				}

				JsonSerializer.Serialize(writer, value, value.GetType(), options);
			}
		}
	}
}
