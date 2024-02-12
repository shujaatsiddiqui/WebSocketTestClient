using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var uri = new Uri("wss://Lane7000-9001.laclubs.com:50000/");

            using (var client = new ClientWebSocket())
            {
                try
                {
                    await client.ConnectAsync(uri, CancellationToken.None);

                    Console.WriteLine("Connected to WebSocket server.");

                    // Send a message (if needed)
                    await client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("Hello from C# WebSocket client!")), WebSocketMessageType.Text, true, CancellationToken.None);

                    // Receive messages
                    while (true)
                    {
                        var result = await client.ReceiveAsync(new ArraySegment<byte>(new byte[1024]), CancellationToken.None);
                        var message = "";//Encoding.UTF8.GetString(result.Array, 0, result.Count);
                        Console.WriteLine("Received message: {0}", message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message + " | " + ex.InnerException);
                    Console.ReadLine();
                }
                finally
                {
                    await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                }
            }
        }
    }
}