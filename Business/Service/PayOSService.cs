using Microsoft.Extensions.Options;
using Model.PayOS;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.Service
{
    public class PayOSService
    {
        private readonly PayOSConfig _config;
        private readonly HttpClient _httpClient;

        public PayOSService(IOptions<PayOSConfig> config)
        {
            _config = config.Value;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api-merchant.payos.vn/")
            };
        }

        public async Task<string> CreatePaymentLink(decimal amount, int orderCode, string description, string buyerName, string returnUrl, string cancelUrl, string signature)
        {
            var body = new
            {
                orderCode = orderCode,
                amount = (int)amount,
                description = description,
                buyerName = buyerName,
                returnUrl = returnUrl,
                cancelUrl = cancelUrl,
                signature = signature
            };

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-client-id", _config.ClientId);
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _config.ApiKey);

            var response = await _httpClient.PostAsync("v2/payment-requests", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"PayOS API Error: {error}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseContent);
            var root = doc.RootElement;

            if (root.TryGetProperty("data", out JsonElement dataElement) &&
                dataElement.TryGetProperty("checkoutUrl", out JsonElement checkoutUrlElement))
            {
                string checkoutUrl = checkoutUrlElement.GetString();
                return checkoutUrl;
            }
            else
            {
                throw new Exception($"Không tìm thấy trường 'checkoutUrl' trong phản hồi PayOS. Response JSON: {responseContent}");
            }
        }

    }
}
