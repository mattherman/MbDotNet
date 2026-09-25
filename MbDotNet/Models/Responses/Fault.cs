using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace MbDotNet.Models.Responses
{
	/// <summary>
	/// Types of faults that can be returned as part of a fault response
	/// </summary>
	public enum Fault
	{
		[JsonStringEnumMemberName("CONNECTION_RESET_BY_PEER")]
		ConnectionResetByPeer,

		[JsonStringEnumMemberName("RANDOM_DATA_THEN_CLOSE")]
		RandomDataThenClose
	}
}
