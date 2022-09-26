using System.Collections.Generic;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
	public class AndPredicate : PredicateBase
	{
		[JsonProperty("and")]
		public IEnumerable<PredicateBase> Predicates { get; private set; }

		public AndPredicate(IEnumerable<PredicateBase> predicates)
		{
			Predicates = predicates;
		}
	}
}
