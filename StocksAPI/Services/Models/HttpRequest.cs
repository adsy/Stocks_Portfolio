using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public static class HttpRequest
    {
        public static async Task<string> SendGetCall(string Uri, Dictionary<string, string> headers = null)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage();

            if (headers == null)
            {
                request.RequestUri = new Uri($"{Uri}");
                request.Method = HttpMethod.Get;
            }
            else
            {
                request.RequestUri = new Uri($"{Uri}");
                request.Method = HttpMethod.Get;

                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }
    }
}