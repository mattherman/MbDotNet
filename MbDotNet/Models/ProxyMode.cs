using System.Runtime.Serialization;

#pragma warning disable CS1591

namespace MbDotNet.Models
{
	/// <summary>
	/// Mode for proxy responses
	/// </summary>
	public enum ProxyMode
	{
		[EnumMember(Value = "proxyOnce")]
		ProxyOnce,

		[EnumMember(Value = "proxyAlways")]
		ProxyAlways,

		[EnumMember(Value = "proxyTransparent")]
		ProxyTransparent
	}
}
