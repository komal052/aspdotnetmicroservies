using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AspnetRunBasics.Extension
{
    public  static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAs<T> (this HttpResponseMessage response)
        {
            if(!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($" something went wrong calling api{response.ReasonPhrase}");
            }
            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            
        }
        public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient,string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content=new  StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            return httpClient.PostAsync(url, content);

        }

        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            return httpClient.PostAsync(url, content);
        }
    }
}
