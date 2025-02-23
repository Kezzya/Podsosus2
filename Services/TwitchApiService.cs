namespace Podsosus2.Services
{
    using System;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class TwitchApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public TwitchApiService(string clientId, string clientSecret)
        {
            _httpClient = new HttpClient();
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        // Получение OAuth токена
        private async Task<string> GetOAuthTokenAsync()
        {
            var tokenUrl = "https://id.twitch.tv/oauth2/token";
            var content = new FormUrlEncodedContent(new[]
            {       new KeyValuePair<string, string>("client_id", _clientId),
            new KeyValuePair<string, string>("client_secret", _clientSecret),
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        });

            var response = await _httpClient.PostAsync(tokenUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get OAuth token: {responseString}");
            }

            var tokenResponse = JsonSerializer.Deserialize<dynamic>(responseString);
            return tokenResponse.access_token;
        }

        // Проверка статуса стримера
        public async Task<bool> IsStreamerLiveAsync(string username)
        {
            try
            {
                var oauthToken = await GetOAuthTokenAsync();
                var apiUrl = $"https://api.twitch.tv/helix/streams?user_login={username}";

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Client-ID", _clientId);
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {oauthToken}");

                var response = await _httpClient.GetAsync(apiUrl);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to fetch stream status: {responseString}");
                }

                var data = JsonSerializer.Deserialize<dynamic>(responseString);

                // Если массив данных не пустой, значит стример онлайн
                return data.data.Count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking stream status for {username}: {ex.Message}");
                return false;
            }
        }
    }
}
