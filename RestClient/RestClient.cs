using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestClient.Attributes;

namespace RestClient
{
    public class RestClient : HttpClient, IRestClient
    {
        private readonly HttpClientOptions _options;

        private Dictionary<string, MethodInfo> _methods = new Dictionary<string, MethodInfo>();

        public RestClient() 
        {
            GetAllClientMethods();
        }

        public RestClient(HttpMessageHandler handler) : base(handler)
        {
            GetAllClientMethods();
        }

        public RestClient(IOptions<HttpClientOptions> options) : base(new HttpClientHandler
        {
            Proxy = CreateProxy(options.Value.Proxy)
        })
        {
            _options = options.Value;
            Timeout = TimeSpan.FromSeconds(Math.Max(_options.Timeout, 10));

            if (!string.IsNullOrEmpty(_options.BaseUrl))
                BaseAddress = new Uri(_options.BaseUrl);

            GetAllClientMethods();
        }

        public async Task<TResponse> SendAsync<TResponse>([CallerMemberName] string memberName = "")
            where TResponse : new()
        {
            return await SendAsync<TResponse>(null, memberName);
        }

        public async Task SendAsync(object request, [CallerMemberName]string memberName = "", string queryString = null, bool deleteFromBody = false)
        {
            await SendAsync<Void>(request, memberName, queryString);
        }

        public async Task SendAsync([CallerMemberName] string memberName = "")
        {
            await SendAsync<Void>(null, memberName);
        }

        public async Task<TResponse> SendAsync<TResponse>(object request, [CallerMemberName] string memberName = "", string queryString = null)
            where TResponse : new()
        {
            var method = _methods[memberName];
            var attribute = method.GetCustomAttribute<HttpAttribute>();

            if (attribute == null)
            {
                throw new ArgumentNullException($"Http attribute was not applied on method '{memberName}'");
            }

            var relativePath = UrlHelpers.ReplaceParameterTemplatesInRelativePathWithValues(attribute.Path, request);

            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Method = attribute.Method;
            AddDefaultRequestHeaders(DefaultRequestHeaders);
           
            if (attribute.Method == HttpMethod.Post || attribute.Method == HttpMethod.Put || (attribute.Method == HttpMethod.Delete))
            {
                if (request != null)
                {
                    var jsonObject = JsonConvert.SerializeObject(request);
                    StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                    requestMessage.Content = content;
                }
            }
            else
            {
                if (request != null && queryString == null)
                {
                    queryString = string.Format("?{0}", request.ToQueryString());
                }
            }

            var uri = BaseAddress != null 
                ? new Uri(string.Format("{0}{1}{2}", BaseAddress, relativePath, queryString)) 
                : new Uri(string.Format("{0}{1}", relativePath, queryString));

            requestMessage.RequestUri = uri;
            var responseMessage = await base.SendAsync(requestMessage);
            return await GetResponse<TResponse>(responseMessage);
        }

        protected virtual void AddDefaultRequestHeaders(HttpRequestHeaders defaultRequestHeaders)
        {
            defaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected virtual async Task<T> GetResponse<T>(HttpResponseMessage response)
            where T : new()
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var errorString = await response.Content.ReadAsStringAsync();
                //// todo error handling
                ////ErrorModel errorModel = null;

                ////try
                ////{
                ////    errorModel = !string.IsNullOrEmpty(errorString)
                ////        ? JsonConvert.DeserializeObject<ErrorModel>(errorString)
                ////        : new ErrorModel();
                ////}
                ////catch (Exception e)
                ////{
                ////    throw new WebApiException(HttpStatusCode.InternalServerError, errorString);
                ////}

                ////throw new WebApiException(response.StatusCode, errorString)
                ////{
                ////    ErrorCode = errorModel.ErrorCode,
                ////    ErrorMessage = errorModel.ErrorMessage
                ////};
            }

            if (typeof(T) == typeof(Void))
            {
                var tcs = new TaskCompletionSource<T>();
                tcs.SetResult(new T());
                return await tcs.Task;
            }

            var jsonString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(jsonString);
            return result;
        }

        protected void GetAllClientMethods()
        {
            AddMethods(GetType().GetRuntimeMethods());
        }

        private static IWebProxy CreateProxy(string proxy)
        {
            if (string.IsNullOrEmpty(proxy))
                return null;

            return new WebProxy(proxy);
        }

        public void AddMethods(IEnumerable<MethodInfo> methodsList)
        {
            string[] exclude = new[]
            {
                "Finalize", nameof(Dispose), nameof(GetHashCode), nameof(GetType),  nameof(MemberwiseClone), nameof(GetStringAsync),
                nameof(SendAsync), nameof(GetStreamAsync), nameof(GetAsync),nameof(PostAsync),
                nameof(PutAsync),nameof(GetByteArrayAsync), nameof(DeleteAsync), nameof(Equals), nameof(ToString)
            };

            foreach (var method in methodsList.Where(x => !exclude.Contains(x.Name)))
            {
                _methods.Add(method.Name, method);
            }
        }
    }
}
