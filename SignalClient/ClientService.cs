using Microsoft.AspNetCore.SignalR.Client;

namespace SignalClient
{
    public class ClientService
    {
        public async Task Call(string groupName)
        {
            var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5173/Chat").Build();
            await connection.StartAsync();

            Console.WriteLine(connection.State);
            var _check_message = "client_0";

            connection.On<string>("ReceiveMessage", async (message) =>
            {
                Console.WriteLine($"{message}");
                Console.WriteLine(message == _check_message);
                if (message == _check_message)
                {
                    string ret_message = message.Replace("serverclient_", "");
                    await connection.InvokeAsync("SendMessage", groupName, "user1", "client1_" + ret_message);
                    string mes = await Test("Group_" + ret_message);
                }
            });

            if (connection.State == HubConnectionState.Connected)
            {
                Console.WriteLine("Connected ... Please type message now");
                Console.WriteLine(connection.ConnectionId);
                await connection.InvokeAsync("JoinGroup", groupName);
                while (true)
                {
                    // await Task.Delay(3000);
                }
            }
        }

        public async Task<string> Test(string groupName)
        {
            Console.WriteLine("Go:" + groupName);
            var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5173/Chat").Build();
            await connection.StartAsync();
            string return_mes = "";
            // string check_mes = "";
            connection.On<string>("ReceiveMessage", async (message) =>
            {
                Console.WriteLine($" Group Name : {groupName} , Message : {message}");
                string mes = await TestMessage(message);
                await connection.InvokeAsync("SendMessage", groupName, "user1", "client1_" + mes);
                Console.WriteLine("SendMessage");
                return_mes = message;
                await connection.InvokeAsync("LeaveGroup", groupName);
                Console.WriteLine("LeaveGroup");
            });

            if (connection.State == HubConnectionState.Connected)
            {
                Console.WriteLine($"Connected ... {groupName}");
                Console.WriteLine(connection.ConnectionId);
                await connection.InvokeAsync("JoinGroup", groupName);
                while (string.IsNullOrEmpty(return_mes))
                {
                    // await Task.Delay(3000);
                }
            }
            return return_mes;
        }

        public async Task<string> TestMessage(string receiveMes)
        {
            await Task.Delay(5000);
            return "_receiveMes : " + receiveMes;
        }
    }
}