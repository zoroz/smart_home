using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RestClient
{
    public interface IRestClient
    {
        Task SendAsync(object request, [CallerMemberName] string memberName = "", string queryString = null, bool deleteFromBody = false);

        Task SendAsync([CallerMemberName] string memberName = "");

        Task<TResponse> SendAsync<TResponse>([CallerMemberName] string memberName = "")
            where TResponse : new();


       Task<TResponse> SendAsync<TResponse>(object request, [CallerMemberName] string memberName = "",string queryString = null)
            where TResponse : new();
    }
}
