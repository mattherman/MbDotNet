using System.Collections.Generic;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
	public class OrPredicate : PredicateBase
	{
		[JsonProperty("or")]
		public IEnumerable<PredicateBase> Predicates { get; private set; }

		public OrPredicate(IEnumerable<PredicateBase> predicates)
		{
			Predicates = predicates;
		}
	}
}
