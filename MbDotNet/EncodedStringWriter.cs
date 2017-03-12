using System.Text;
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
