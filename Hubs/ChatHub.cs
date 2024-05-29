using formulaOne.chatService.DataService;
using formulaOne.chatService.Models;
using Microsoft.AspNetCore.SignalR;

namespace formulaOne.chatService.Hubs;

public class ChatHub: Hub
{
    private readonly SharedDb _shared;
    public ChatHub(SharedDb shared) => _shared = shared;
    
    public async Task JoinChat(UserConnection conn)
    {
        await Clients.All.SendAsync("ReceiveMessage", "admin", $"{conn.Username} has Joined");
        
    }

    public async Task JoinSpecificChatRoom(UserConnection conn)
    {
        
        await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);
        _shared.connections[Context.ConnectionId] = conn;
        await Clients.Group(conn.ChatRoom)
            .SendAsync("JoinSpecificChatRoom", "admin", $"{conn.Username} has Joined {conn.ChatRoom}");
        
    }

    public async Task SendMessage(String msg)
    {
        if (_shared.connections.TryGetValue(Context.ConnectionId, out UserConnection conn))
        {
            await Clients.Group(conn.ChatRoom)
                .SendAsync("ReceiveSpecificMessage", conn.Username, msg);
        }
    }
}