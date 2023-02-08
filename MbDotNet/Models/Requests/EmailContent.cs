using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

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
		[JsonProperty("type")]
		public string Type { get; internal set; }

		/// <summary>
		/// The binary data of the content
		/// </summary>
		[JsonProperty("data")]
		public byte[] Data { get; internal set; }
	}
}
