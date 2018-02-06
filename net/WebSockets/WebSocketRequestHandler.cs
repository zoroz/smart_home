using System;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SmartHome.WebSockets
{
    public interface IWebSocketRequestHandler
    {
        Task Handle(HttpContext context, WebSocket webSocket);
    }

    public class WebSocketRequestHandler : IWebSocketRequestHandler
    {
        private readonly ILogger<WebSocketRequestHandler> _log;

        public WebSocketRequestHandler(ILogger<WebSocketRequestHandler> log)
        {
            _log = log;
        }

        public async Task Handle(HttpContext context, WebSocket webSocket)
        {
            _log.LogInformation("Handling web socket");
            /*We define a certain constant which will represent
            size of received data. It is established by us and 
            we can set any value. We know that in this case the size of the sent
            data is very small.
            */
            const int maxMessageSize = 1024;

            //Buffer for received bits.
            var receivedDataBuffer = new ArraySegment<Byte>(new Byte[maxMessageSize]);

            var cancellationToken = new CancellationToken();

            //Checks WebSocket state.
            while (webSocket.State == WebSocketState.Open)
            {
                
                //Reads data.
                WebSocketReceiveResult webSocketReceiveResult =
                    await webSocket.ReceiveAsync(receivedDataBuffer, cancellationToken);

                //If input frame is cancelation frame, send close command.
                if (webSocketReceiveResult.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                        string.Empty, cancellationToken);

                    return;
                }

                byte[] payloadData = receivedDataBuffer.Array.Where(b => b != 0).ToArray();

                //Because we know that is a string, we convert it.
                string receiveString =
                    System.Text.Encoding.UTF8.GetString(payloadData, 0, payloadData.Length);

                _log.LogInformation(receiveString);
                //todo Zoro: execute ws handler for command
            }
        }
    }
}