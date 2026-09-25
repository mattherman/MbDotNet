using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

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
		[JsonPropertyName("address")]
		public string Address { get; set; }

		/// <summary>
		/// A name associated with the email address
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }
	}
}
