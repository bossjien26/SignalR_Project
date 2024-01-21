using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalClient
{
    public class ClientService
    {
        public async Task Call()
        {
            var connection = new HubConnectionBuilder().WithUrl("http://localhost:5173/Chat").Build();
            // .WithUrl("https://your-app-url/chatHub")
            // .Build();
            await connection.StartAsync();

            Console.WriteLine(connection.State);
            string _check_message = "serverclient1";
            string _return_message = "";
            connection.On<string>("ReceiveMessage", async (message) =>
            {
                Console.WriteLine($"{message}");
                if (message.Contains(_check_message))
                {
                    _return_message = _check_message;
                    await connection.DisposeAsync();
                    // return "test";
                }
            });

            if (connection.State == HubConnectionState.Connected)
            {
                Console.WriteLine("Connected ... Please type message now");


                Console.WriteLine(connection.ConnectionId);
                await connection.InvokeAsync("JoinGroup", "Group1");
                // await connection.InvokeAsync("SendMessage", "Group1", DateTime.Now.ToString("HH:mm:ss"));
                int i = 0;
                bool _status = true;
                await connection.InvokeAsync("SendMessage", "Group1", "user2", "client_" + i.ToString());

                while (_status)
                {
                    if (connection.State != HubConnectionState.Connected)
                    {
                        _status = false;
                        break;
                    }
                    // await Task.Delay(30000);
                }

                Console.WriteLine("Diconnected");
                // await connection.InvokeAsync("SendMessage", connection.ConnectionId, "Hello, User2!");
                // Console.WriteLine(_message);
                // wait SendMessage
                // await task;

                // await connection.InvokeAsync("LeaveGroup", "Group1");

                // await connection.DisposeAsync();
            }
        }
    }
}