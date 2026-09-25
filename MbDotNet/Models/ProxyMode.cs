using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace MbDotNet.Models
{
	/// <summary>
	/// Mode for proxy responses
	/// </summary>
	public enum ProxyMode
	{
		[JsonStringEnumMemberName("proxyOnce")]
		ProxyOnce,

		[JsonStringEnumMemberName("proxyAlways")]
		ProxyAlways,

		[JsonStringEnumMemberName("proxyTransparent")]
		ProxyTransparent
	}
}
