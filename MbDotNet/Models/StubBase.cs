using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MbDotNet.Interfaces;

namespace MbDotNet.Models
{
    public abstract class StubBase
    {
        public ICollection<IPredicate> Predicates { get; set; }
        public ICollection<IResponse> Responses { get; set; }
    }
}
