using ACE_App.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;


namespace ACE_App.Services
{
    public interface IACEService
    {
        public Task<ACEModel> GetEstimation(string userText);
    }
    public class ACEService: IACEService
    {
        private readonly HttpClient client;
        private readonly ILogger<ACEService> logger;
        public ACEService(HttpClient client, ILogger<ACEService> logger)
        {
            this.client = client;
            this.logger = logger;
        }
        public async Task<ACEModel> GetEstimation(string userText)
        {
            try
            {
                ACEModel model = new ACEModel();
                model.UserInput = userText;
                string requestJson = JsonConvert.SerializeObject(model);
                //  var jsonObj = JsonSerializer // TODO - Please check this syntax - convert the above model into json object when hari's API is ready.
                var foo = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                string requestUrl = "http://127.0.0.1:8000/predictexp";
                HttpResponseMessage response = null;
                var data = "";

                var content = new StringContent(requestJson);
                content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                response = await client.PostAsync(requestUrl, content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    model = JsonConvert.DeserializeObject<ACEModel>(data);
                    logger.LogInformation("model generation successful");

                }
                else
                    throw new Exception("something wrong");

                return model;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new ACEModel() { ErrorMessage = "Request could not be processed" };
            }

        }
    }
}
