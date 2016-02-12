using MbDotNet.Interfaces;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
    public class EqualsPredicate : IPredicate
    {
        [JsonProperty("equals")]
        private EqualsPredicateDetail _detail;

        public EqualsPredicate()
        {
            _detail = new EqualsPredicateDetail();
        }

        internal class EqualsPredicateDetail
        {

        }
    }
}
