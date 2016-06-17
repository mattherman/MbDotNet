using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
    public class NotPredicate : PredicateBase
    {
        [JsonProperty("not")]
        public PredicateBase ChildPredicate { get; private set; }

        public NotPredicate(PredicateBase childPredicate)
        {
            ChildPredicate = childPredicate;
        }
    }
}
