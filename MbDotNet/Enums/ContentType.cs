using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbDotNet.Enums
{
    public class ContentType
    {
        public static readonly ContentType Json = new ContentType("application/json");
        public static readonly ContentType Xml = new ContentType("application/xml");

        public string ContentTypeString { get; private set; }

        public ContentType(string contentTypeString)
        {
            ContentTypeString = contentTypeString;
        }
    }
}
