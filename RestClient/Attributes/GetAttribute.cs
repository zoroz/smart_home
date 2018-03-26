using System.Net.Http;

namespace RestClient.Attributes
{
    public class GetAttribute : HttpAttribute
    {
        public GetAttribute(string path)
            : base(path)
        {
        }

        public override HttpMethod Method => HttpMethod.Get;
    }
}
