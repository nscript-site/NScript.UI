using System.Collections.Generic;

namespace ExCSS
{
    internal static class PortNumbers
    {
        private static readonly Dictionary<string, string> Ports = new Dictionary<string, string>
        {
            {ProtocolNames.Http, "80"},
            {ProtocolNames.Https, "443"},
            {ProtocolNames.Ftp, "21"},
            {ProtocolNames.File, ""},
            {ProtocolNames.Ws, "80"},
            {ProtocolNames.Wss, "443"},
            {ProtocolNames.Gopher, "70"},
            {ProtocolNames.Telnet, "23"},
            {ProtocolNames.Ssh, "22"}
        };

        public static string GetDefaultPort(string protocol)
        {
            string value;
            Ports.TryGetValue(protocol, out value);
            return value;
        }
    }
}