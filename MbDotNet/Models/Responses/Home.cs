using System;
using Newtonsoft.Json;

namespace MbDotNet.Models.Responses
{
	public class Home
	{
		[JsonProperty("_links")]
		public Link Links { get; internal set; }

	}

	public class Link
	{
		[JsonProperty("imposters")]
		public HrefField Imposters { get; internal set; }
		[JsonProperty("config")]
		public HrefField Config { get; internal set; }
		[JsonProperty("logs")]
		public HrefField Logs { get; internal set; }

	}

	public class HrefField
	{
		[JsonProperty("href")]
		public string Href { get; internal set; }


	}
}

