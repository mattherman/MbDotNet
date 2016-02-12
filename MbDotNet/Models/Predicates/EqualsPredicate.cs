using MbDotNet.Interfaces;
using Newtonsoft.Json;

namespace MbDotNet
{
    public class EqualsPredicate : IPredicate
    {
        [JsonProperty("equals")]
        private EqualsPredicateDetail detail;

        public EqualsPredicate()
        {
        }

        internal class EqualsPredicateDetail
        {

        }
    }
}
