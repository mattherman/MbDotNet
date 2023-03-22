namespace MbDotNet.Models.Responses.Fields
{
	/// <summary>
	/// Represents a type that supports default response fields
	/// </summary>
	/// <typeparam name="TFields"></typeparam>
	public interface IWithResponseFields<out TFields> where TFields : ResponseFields
	{
		/// <summary>
		/// Optional default response that imposter sends back if no predicate matches a request
		/// </summary>
		TFields DefaultResponse { get; }
	}
}
