using System.Net.Http;

namespace SmartHome.Infrastucture.Attributes
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
