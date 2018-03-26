using System.Net.Http;

namespace RestClient.Attributes
{
    public class PostAttribute : HttpAttribute
    {
        public PostAttribute(string path)
            : base(path)
        {
        }

        public override HttpMethod Method => HttpMethod.Post;
    }
}
