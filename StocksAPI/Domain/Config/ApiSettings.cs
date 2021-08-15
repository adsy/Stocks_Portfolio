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
        public Dictionary<string, string> Headers { get; set; }
    }
}