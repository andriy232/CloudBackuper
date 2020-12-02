using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using DataGuardian.Server.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace DataGuardian.Server.Services
{
    public class PingService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TcpListener listener;

            while (true)
            {
                try
                {
                    if (DataGuardianController.Port == default)
                    {
                        await Task.Delay(1000, stoppingToken);
                        continue;
                    }

                    listener = new TcpListener(IPAddress.Any, DataGuardianController.Port);
                    listener.Start();
                    break;
                }
                catch
                {
                    // ignored
                }
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                var client = await listener.AcceptTcpClientAsync();
                var stream = client.GetStream();

                while (!stoppingToken.IsCancellationRequested)
                {
                    var data = new byte[1024];
                    var read = await stream.ReadAsync(data, 0, 1024, stoppingToken);

                    await stream.WriteAsync(data, 0, read, stoppingToken);
                }
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}