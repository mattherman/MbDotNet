using System;
using System.Text.Json.Serialization;

namespace MbDotNet.Models.Responses
{
	/// <summary>
	/// A Mountebank server log
	/// </summary>
	public class Log
	{
		/// <summary>
		/// The log level
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("level")]
		public string Level { get; internal set; }

		/// <summary>
		/// The log message
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("message")]
		public string Message { get; internal set; }

		/// <summary>
		/// When the log was recorded
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("Timestamp")]
		public DateTime Timestamp { get; internal set; }
	}
}
