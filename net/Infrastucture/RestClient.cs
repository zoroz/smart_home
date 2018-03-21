﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartHome.Infrastucture.Attributes;

namespace SmartHome.Infrastucture
{
    public class RestClient
    {
        private string _webApiBaseUrl;

        private Dictionary<string, MethodInfo> _methods = new Dictionary<string, MethodInfo>();

        public RestClient(HttpClient httpClient)
        {
            Client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            if (httpClient.BaseAddress != null)
            {
                _webApiBaseUrl = httpClient.BaseAddress.ToString();
                if (!_webApiBaseUrl.EndsWith("/"))
                {
                    _webApiBaseUrl = $"{_webApiBaseUrl}/";
                }
            }

            GetAllClientMethods();
        }

        public string Token { get; set; }

        public string WebApiBaseUrl
        {
            get => _webApiBaseUrl;

            set
            {
                _webApiBaseUrl = value;
                Client.BaseAddress = new Uri(value);
            }
        }

        public TimeSpan Timeout
        {
            get => Client.Timeout;
            set => Client.Timeout = value;
        }

        protected HttpClient Client { get; set; }

        internal async Task<TResponse> SendAsync<TResponse>([CallerMemberName] string memberName = "")
            where TResponse : new()
        {
            return await SendAsync<TResponse>(null, memberName);
        }

        internal async Task SendAsync(object request, [CallerMemberName] string memberName = "",
            string queryString = null, bool deleteFromBody = false)
        {
            await SendAsync<Void>(request, memberName, queryString);
        }

        internal async Task SendAsync([CallerMemberName] string memberName = "")
        {
            await SendAsync<Void>(null, memberName);
        }

        internal async Task<TResponse> SendAsync<TResponse>(object request, [CallerMemberName] string memberName = "", string queryString = null)
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
            //// todo 
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("sign", "qvpP9CHX/PUccoFpbpRzDPmFe2PVYZ2pATCP/3kzyMk=");
            if (attribute.Method == HttpMethod.Post || attribute.Method == HttpMethod.Put ||
                (attribute.Method == HttpMethod.Delete))
            {
                if (request != null)
                {
                    var jsonObject = JsonConvert.SerializeObject(request);
                    StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                    requestMessage.Content = content;
                }

                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            else
            {
                if (request != null && queryString == null)
                {
                    queryString = string.Format("?{0}", request.ToQueryString());
                }
            }

            var uri = new Uri(string.Format("{0}{1}{2}", WebApiBaseUrl, relativePath, queryString));
            requestMessage.RequestUri = uri;
            var responseMessage = await Client.SendAsync(requestMessage);
            return await GetResponse<TResponse>(responseMessage);
        }

        protected async Task<T> GetResponse<T>(HttpResponseMessage response)
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

        private void AddMethods(IEnumerable<MethodInfo> methodsList)
        {
            string[] exclude = new[]
            {
                "ToString", "Equals", "GetHashCode", "GetType", "Finalize", "MemberwiseClone",
                nameof(RestClient.SendAsync)
            };

            foreach (var method in methodsList.Where(x => !exclude.Contains(x.Name)))
            {
                _methods.Add(method.Name, method);
            }
        }
    }
}
