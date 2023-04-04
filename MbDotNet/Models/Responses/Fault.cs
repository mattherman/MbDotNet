using System.Runtime.Serialization;

#pragma warning disable CS1591

namespace MbDotNet.Models.Responses
{
	/// <summary>
	/// Types of faults that can be returned as part of a fault response
	/// </summary>
	public enum Fault
	{
		[EnumMember(Value = "CONNECTION_RESET_BY_PEER")]
		ConnectionResetByPeer,

		[EnumMember(Value = "RANDOM_DATA_THEN_CLOSE")]
		RandomDataThenClose
	}
}
