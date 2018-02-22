using System.Runtime.Serialization;

namespace MbDotNet.Enums
{
    public enum ProxyMode
    {
        [EnumMember(Value = "proxyOnce")]
        ProxyOnce,

        [EnumMember(Value = "proxyAlways")]
        ProxyAlways,

        [EnumMember(Value = "proxyTransparent")]
        ProxyTransparent
    }
}
