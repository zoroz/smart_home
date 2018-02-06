using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartHome.WebSockets;
using WebSocketMiddleware = Microsoft.AspNetCore.WebSockets.WebSocketMiddleware;

namespace SOnOffServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddMvc();
            
            services.AddTransient<WebSocketRequestHandler>();
            //services.AddTransient<WebSocketMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseWebSockets();
            app.Map("", builder =>

            {

                builder.Use(async (context, next) =>

                {

                    if (context.WebSockets.IsWebSocketRequest)

                    {

                        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        var log = context.RequestServices.GetService<ILogger<WebSocketRequestHandler>>();
                        await Echo(log, webSocket);

                        return;

                    }

                    await next();

                });

            });
            app.UseMvc();
        }

        private async Task Echo(ILogger log, WebSocket webSocket)

        {

            byte[] buffer = new byte[1024 * 4];

            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)

            {
                string data = Encoding.UTF8.GetString(buffer, 0, result.Count);
                log.LogInformation($"Received {data}");
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
