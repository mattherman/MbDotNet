using System.Collections.Generic;
using Newtonsoft.Json;

namespace MbDotNet.Models
{
	/// <summary>
	/// The configuration of the Mountebank server
	/// </summary>
	public class Config
	{
		/// <summary>
		/// The version of Mountebank that is running
		/// </summary>
		[JsonProperty("version")]
		public string Version { get; set; }

		/// <summary>
		/// The command line options used to start Mountebank
		/// </summary>
		[JsonProperty("options")]
		public Dictionary<string, dynamic> Options { get; set; }

		/// <summary>
		/// Information about the running Mountebank process
		/// </summary>
		[JsonProperty("process")]
		public Dictionary<string, dynamic> Process { get; set; }
	}
}
