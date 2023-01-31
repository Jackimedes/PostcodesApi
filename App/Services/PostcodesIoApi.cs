using PostcodesApi.App.Common.Responses.PostcodesIoApi;
using PostcodesApi.App.Services.Contracts;
using Newtonsoft.Json;
using System.Web.Http;

namespace PostcodesApi.App.Services
{
    public class PostcodesIoApi : IPostcodesIoApi
    {
        private readonly HttpClient _httpClient;

        public PostcodesIoApi(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<GetPostcodeResponse> Lookup(string postcode)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/postcodes/{postcode}");

            if (!response.IsSuccessStatusCode)
                throw new HttpResponseException(response);

            string result = await response.Content.ReadAsStringAsync();
            GetPostcodeResponse getPostcodeResponse = JsonConvert.DeserializeObject<GetPostcodeResponse>(result)!;
            getPostcodeResponse.StatusCode = response.StatusCode;

            return getPostcodeResponse;
        }
    }
}
