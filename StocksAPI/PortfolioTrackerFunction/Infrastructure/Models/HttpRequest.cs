using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioTrackerFunction.Models
{
    public static class HttpRequest
    {
        public static async Task<Response<string>> SendGetCall(string Uri, Dictionary<string, string> headers = null)
        {
            var fnResult = new Response<string>
            {
                StatusCode = (int)HttpStatusCode.OK
            };

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
            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    fnResult.Data = await response.Content.ReadAsStringAsync();
                    return fnResult;
                }
            }
            catch (HttpRequestException ex)
            {
                fnResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                fnResult.Message = ex.Message;
                return fnResult;
            }
        }
    }
}