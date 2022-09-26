using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MbDotNet.Models.Requests
{
	public class EmailContent
	{
		[JsonProperty("type")]
		public string Type { get; internal set; }

		[JsonProperty("data")]
		public byte[] Data { get; internal set; }
	}
}
