namespace SmartHome.Options
{
    public class HttpClientOptions
    {
        public string BaseUrl { get; set; }

        public string Proxy { get; set; }
        
        public int Timeout { get; set; }
    }

    public class SOnOffHttpClientOptions : HttpClientOptions
    { }

    public class SeltronHttpClientOptions : HttpClientOptions
    { }
}
