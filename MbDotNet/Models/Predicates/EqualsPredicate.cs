using MbDotNet.Interfaces;
using MbDotNet.Models.Predicates;

namespace MbDotNet
{
    public class EqualsPredicate : IPredicate
    {
        public EqualsPredicateDetail Detail { get; private set; }

        public EqualsPredicate(EqualsPredicateDetail detail)
        {
            Detail = detail;
        }
    }
}
