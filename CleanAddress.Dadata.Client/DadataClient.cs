using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace CleanAddress.Dadata.Client
{
    public class DadataClient : IDadataClient
    {
        private readonly HttpClient _client;
        private readonly ClientOption _option;
        private readonly ILogger<DadataClient> _logger;
        public DadataClient(HttpClient client, IOptions<ClientOption> options, ILogger<DadataClient> logger)
        {
            _client = client;
            _option = options.Value;
            _logger = logger;
        }

        public async Task<CleanAddressDto?> GetStandardizedAddress(string address)
        {
            _client.DefaultRequestHeaders.Add("Authorization", $"Token {_option.Token}");
            _client.DefaultRequestHeaders.Add("X-Secret", _option.Secret);

            try
            {
                var response = await _client.PostAsJsonAsync(string.Empty, new List<string> { address });
                var cleanAddressDto = await response.Content.ReadFromJsonAsync<List<CleanAddressDto>>();

                return cleanAddressDto!.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
