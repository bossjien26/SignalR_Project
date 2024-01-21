using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClient_API
{
    public class ClientService
    {
        public async Task<string> Call(string groupName, string checkMes, string sendMes)
        {
            var connection = new HubConnectionBuilder().WithUrl("http://localhost:5173/Chat").Build();
            // .WithUrl("https://your-app-url/chatHub")
            // .Build();
            await connection.StartAsync();

            Console.WriteLine(connection.State);
            string _check_message = checkMes;//"serverclient1";
            string _return_message = "";
            connection.On<string>("ReceiveMessage", async (message) =>
            {
                Console.WriteLine($"{message}");
                if (message.Contains(_check_message))
                {
                    _return_message = message;
                    await connection.DisposeAsync();
                    // return "test";
                }
            });

            if (connection.State == HubConnectionState.Connected)
            {
                Console.WriteLine("Connected ... Please type message now");


                Console.WriteLine(connection.ConnectionId);
                await connection.InvokeAsync("JoinGroup", groupName);
                // await connection.InvokeAsync("SendMessage", "Group1", DateTime.Now.ToString("HH:mm:ss"));
                int i = 0;
                bool _status = true;

                while (_status)
                {
                    if (connection.State != HubConnectionState.Connected)
                    {
                        _status = false;
                        Console.WriteLine("Diconnected");
                        return _return_message;
                    }
                    else
                    {
                        await connection.InvokeAsync("SendMessage", groupName, "user2", sendMes + i.ToString());
                        await Task.Delay(3000);
                    }
                }

            }
            Console.WriteLine("Diconnected");
            return _return_message;
        }
    }
}