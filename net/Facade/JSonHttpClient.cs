using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestClient;
using SmartHome.Infrastucture;
using SmartHome.Options;

namespace SmartHome.Facade
{
    public abstract class JsonHttpClient : RestClient.RestClient
    {
        protected JsonHttpClient(IOptions<HttpClientOptions> options) : base(options)
        {
        }

        protected async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest data) where TResponse : class
        {
            AddDefaultRequestHeaders(DefaultRequestHeaders);
            HttpContent content = PrepareContent(data);
            AddRequestContentHeaders(content);
            HttpResponseMessage response = await PostAsync(url, content);
            Stream stream = await GetStreamAndTraceResponseAsync(response);

            return DeserializeInternal<TResponse>(stream);
        }

        protected async Task<TResponse> GetAsync<TResponse>(string url) where TResponse : class
        {
            AddDefaultRequestHeaders(DefaultRequestHeaders);
            HttpResponseMessage response = await GetAsync(url);
            Stream stream = await GetStreamAndTraceResponseAsync(response);
            return DeserializeInternal<TResponse>(stream);
        }

        private Task<Stream> GetStreamAndTraceResponseAsync(HttpResponseMessage response)
        {
            return response.Content.ReadAsStreamAsync();
        }

        private HttpContent PrepareContent<T>(T data)
        {
            using (MemoryStream stream = new MemoryStream())
            using (StreamContent sc = new StreamContent(stream))
            {
                SerializeInternal(data, stream);
                return sc;
            }
        }

        protected virtual void AddRequestContentHeaders(HttpContent httpContent)
        {
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }

        protected virtual TResponse DeserializeInternal<TResponse>(Stream stream)
            where TResponse : class
        {
            JsonSerializer serializer = new JsonSerializer();
            using (var sw = new StreamReader(stream))
            {
                JsonReader reader = new JsonTextReader(sw);

                return serializer.Deserialize<TResponse>(reader);
            }
        }

        protected virtual void SerializeInternal<TRequest>(TRequest data, MemoryStream stream)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(stream, new UTF8Encoding(false), 4096, true))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, data);
                writer.Flush();
                sw.Flush();
                stream.Flush();
                stream.Position = 0;
            }
        }
    }
}
