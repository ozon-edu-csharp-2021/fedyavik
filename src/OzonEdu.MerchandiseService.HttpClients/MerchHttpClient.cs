using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using OzonEdu.MerchandiseService.Models;

namespace OzonEdu.MerchandiseService.HttpClients
{
    public interface IMerchHttpClient
    {
        Task<RequsetMerchResponce> GetRequestMerch(CancellationToken token);
        Task<RequsetMerchResponce> GetIssuingMerchInfo(CancellationToken token);
    }

    public class MerchHttpClient : IMerchHttpClient
    {
        private readonly HttpClient _httpClient;

        public MerchHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RequsetMerchResponce> GetRequestMerch(CancellationToken token)
        {
            using var response = await _httpClient.GetAsync("v1/api/stocks/1", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<RequsetMerchResponce>(body);
        }

        public async Task<RequsetMerchResponce> GetIssuingMerchInfo(CancellationToken token)
        {
            HttpContent content = new StringContent("test1");
            using var response = await _httpClient.PostAsync("v1/api/stocks/", content, token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<RequsetMerchResponce>(body);
        }
    }
}