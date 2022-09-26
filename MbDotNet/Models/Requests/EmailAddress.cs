using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MbDotNet.Models.Requests
{
	public class EmailAddress
	{
		[JsonProperty("address")]
		public string Address { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
