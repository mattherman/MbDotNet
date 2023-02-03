using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MbDotNet.Models.Requests
{
	/// <summary>
	/// An email address
	/// </summary>
	public class EmailAddress
	{
		/// <summary>
		/// An email address
		/// </summary>
		[JsonProperty("address")]
		public string Address { get; set; }

		/// <summary>
		/// A name associated with the email address
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
