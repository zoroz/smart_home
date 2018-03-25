namespace RestClient
{
    public class HttpClientOptions
    {
        public string BaseUrl { get; set; }

        public string Proxy { get; set; }
        
        public int Timeout { get; set; }
    }
}
