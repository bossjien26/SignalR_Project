using Microsoft.AspNetCore.SignalR;

namespace SignalRHub_project;

public class ChatHub : Hub
{
    public async Task JoinGroup(string groupName)
    {
        Console.WriteLine("join :" + groupName);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task LeaveGroup(string groupName)
    {
        Console.WriteLine("leave :" + groupName);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task SendMessage(string groupName, string userId, string message)
    {
        Console.WriteLine(userId);
        // await Clients.User(userId).SendAsync("ReceiveMessageUser", "testMessage");
        await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
    }

    // public async Task SendMessage(string user, string message)
    // {
    //     // Thread.Sleep(5000);
    //     // Console.WriteLine(message);
    //     // Console.WriteLine(user);
    //     // await Clients.User(user).SendAsync("ReceiveMessage", message);
    //     var connectionId = Context.ConnectionId;
    //     // _logger.LogInformation($"連接成功: {connectionId}");
    //     await Clients.Caller.SendAsync("ReceiveMessage", "連接成功"); // 傳送訊息給呼叫者

    // }
    // Add other methods for receiving messages from clients here
}