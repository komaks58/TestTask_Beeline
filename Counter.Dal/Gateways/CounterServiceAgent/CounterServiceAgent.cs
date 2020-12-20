using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Counter.Domain.Gateways.CounterServiceAgent;
using Counter.Domain.Services.CounterService.Models;
using Newtonsoft.Json;

namespace Counter.Dal.Gateways.CounterServiceAgent
{
    public class CounterServiceAgent : ICounterServiceAgent
    {
        private readonly HttpClient _httpClient;

        public CounterServiceAgent(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetCurrentTotalValue()
        {
            var response = await _httpClient.GetAsync("counter");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var totalValue = int.Parse(responseContent);

            return totalValue;
        }

        public async Task Count(CountRequest request)
        {
            var requestContent = JsonConvert.SerializeObject(request);

            var response = await _httpClient.PostAsync("counter", new JsonContent(requestContent));
            response.EnsureSuccessStatusCode();
        }
    }

    public class JsonContent : StringContent
    {
        public JsonContent(string content) : base(content, Encoding.UTF8, "application/json")
        {

        }
    }
}
