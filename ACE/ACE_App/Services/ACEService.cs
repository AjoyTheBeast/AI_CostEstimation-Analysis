using ACE_App.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ACE_App.Services
{
    public interface IACEService
    {
        Task<ACEModel> GetEstimation(string userText);
    }

    public class ACEService : IACEService
    {
        private readonly HttpClient _client;
        private readonly ILogger<ACEService> _logger;

        public ACEService(HttpClient client, ILogger<ACEService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<ACEModel> GetEstimation(string userText)
        {
            try
            {
                // Create and serialize request payload
                var requestModel = new ACEModel { UserInput = userText };
                var requestJson = JsonConvert.SerializeObject(requestModel);
                

                var requestUrl = new Uri("http://127.0.0.1:8000/predictexp");  // Update to your FastAPI URL
                var content = new StringContent(requestJson);
                content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

                // Send POST request to FastAPI
                var response = await _client.PostAsync(requestUrl, content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var model = JsonConvert.DeserializeObject<ACEModel>(responseData);
                    _logger.LogInformation("Received successful response from FastAPI.");
                    return model;
                }
                else
                {
                    _logger.LogError($"Failed API call with status code: {response.StatusCode}");
                    return new ACEModel { ErrorMessage = "Failed to retrieve estimation." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in GetEstimation.");
                return new ACEModel { ErrorMessage = "An error occurred while processing your request." };
            }
        }
    }
}
