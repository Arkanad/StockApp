using Microsoft.Extensions.Configuration;
using ServiceContracts;
using System.Text.Json;

namespace Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        async Task<Dictionary<string, object>?> IFinnhubService.GetCompanyProfile(string stockSymbol)
        {
            using(HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("\"https://finnhub.io/api/v1/quote?symbol=AAPL&token=ckvvjcpr01qhjei2s48gckvvjcpr01qhjei2s490\"\r\n")
                };

                HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage);

                Stream streamResponse = httpResponseMessage.Content.ReadAsStream();

                StreamReader streamReaderResponse = new StreamReader(streamResponse);

                string response = streamReaderResponse.ReadToEnd();

                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if(responseDictionary == null)
                {
                    throw new NullReferenceException("No response from FinnhubService!");
                }

                if (responseDictionary.ContainsKey("error"))
                {
                    throw new InvalidOperationException(responseDictionary["error"].ToString());
                }

                return responseDictionary;
            }
        }

        async Task<Dictionary<string, object>?> IFinnhubService.GetStockPriceQuote(string stockSymbol)
        {
            using(HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://finnhub.io/api/v1/quote?symbol=AAPL&token=ckvvjcpr01qhjei2s48gckvvjcpr01qhjei2s490"),
                    Method = HttpMethod.Get
                };

                HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage);

                Stream streamResponseMessage = httpResponseMessage.Content.ReadAsStream();

                StreamReader streamReaderResponse = new StreamReader(streamResponseMessage);

                string response = streamReaderResponse.ReadToEnd();

                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if(responseDictionary == null)
                {
                    throw new NullReferenceException("No response from FinnhubService!");
                }

                if (responseDictionary.ContainsKey("error"))
                {
                    throw new InvalidOperationException(responseDictionary["error"].ToString());
                }

                return responseDictionary;
            }
        }
    }
}
