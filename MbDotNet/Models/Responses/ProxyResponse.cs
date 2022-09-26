using MbDotNet.Models.Responses.Fields;
using Newtonsoft.Json;

namespace MbDotNet.Models.Responses
{
	public class ProxyResponse<T> : ResponseBase where T : ResponseFields, new()
	{
		[JsonProperty("proxy")]
		public T Fields { get; set; }

		public ProxyResponse(T fields)
		{
			Fields = fields;
		}
	}
}
