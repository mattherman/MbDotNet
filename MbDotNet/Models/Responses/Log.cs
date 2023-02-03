using System;
using Newtonsoft.Json;

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
		[JsonProperty("level")]
		public string Level { get; internal set; }

		/// <summary>
		/// The log message
		/// </summary>
		[JsonProperty("message")]
		public string Message { get; internal set; }

		/// <summary>
		/// When the log was recorded
		/// </summary>
		[JsonProperty("Timestamp")]
		public DateTime Timestamp { get; internal set; }
	}
}
