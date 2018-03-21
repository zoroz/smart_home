using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SmartHome.Infrastucture
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
