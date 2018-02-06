using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SmartHome.WebSockets
{
    public class WebSocketMiddleware
    {
        private readonly IWebSocketRequestHandler _handler;
        private readonly ILogger<WebSocketMiddleware> _log;
        private readonly RequestDelegate _next;

        public WebSocketMiddleware(IWebSocketRequestHandler handler)
        {
            _handler = handler;
        }

        public WebSocketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await _handler.Handle(context, webSocket);
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
