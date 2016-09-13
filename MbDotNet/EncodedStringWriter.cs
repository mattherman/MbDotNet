using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MbDotNet
{
    internal class EncodedStringWriter : StringWriter
    {
        public EncodedStringWriter(Encoding enc)
        {
            encoding = enc;
        }

        private Encoding encoding;

        public override Encoding Encoding { get { return encoding; } }
    }
}
