using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TokenConsumer.Dto;

namespace TokenConsumer.Logic
{
    public class PublicKeyProvider : IPublicKeyProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PublicKeyProvider(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        
        public async Task<string> GetPublicKey()
        {
            var publicKeyUri = _configuration["Jwt:PublicKeyUri"];
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync(publicKeyUri);
            var responseContent = await response.Content.ReadAsStringAsync();
            var publicKeyResult = JsonSerializer.Deserialize<KeyResponse>(responseContent);

            return publicKeyResult.Key;
        }
    }
}