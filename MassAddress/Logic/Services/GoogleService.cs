using MassAddress.Models.Config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MassAddress.Logic.Services
{
    public class GoogleService
    {
        private string _apiKey;
        private string _apiUrl;
        private readonly IHttpClientFactory _clientFactory;

        public GoogleService(IOptions<GoogleConfigModel> config, IHttpClientFactory clientFactory)
        {
            _apiKey = config?.Value?.ApiKey;
            _apiUrl = config?.Value?.ApiUrl;
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// Function to create a task which makes an Http request out to a Google endpoint and returns the response.
        /// </summary>
        /// <param name="input">IEnumerable of all the values to replace in the _apiUrl parameter in case different endpoints have different number of parameters</param>
        /// <returns></returns>
        public async Task<string> MakeRequestTask(IEnumerable<string> input)
        {
            if (string.IsNullOrEmpty(_apiKey) || string.IsNullOrEmpty(_apiUrl) || !input.Any())
            {
                return string.Empty;
            }
            try
            {
                var inputArray = input.Append(_apiKey).ToArray();
                var request = new HttpRequestMessage(HttpMethod.Get, string.Format(_apiUrl, inputArray));

                var client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);

                return response != null && response.IsSuccessStatusCode ? await response?.Content?.ReadAsStringAsync() : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
