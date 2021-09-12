using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Config
{
    public class ApiSettings
    {
        public string ClientName { get; set; }
        public string BaseClient { get; set; }
        public Dictionary<string, string> Endpoints { get; set; }
        public Dictionary<String, string> Headers { get; set; }

        protected string GetEndpointUri(string uriKey)
        {
            if (Endpoints != null && Endpoints.ContainsKey(uriKey))
                return Endpoints[uriKey];
            return string.Empty;
        }

        protected string GetEndpointHeader(string headerKey)
        {
            if (Headers != null && Headers.ContainsKey(headerKey))
            {
                return Headers[headerKey];
            }
            return string.Empty;
        }
    }
}