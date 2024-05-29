using System.Collections.Concurrent;
using formulaOne.chatService.Models;

namespace formulaOne.chatService.DataService;

public class SharedDb
{
    private readonly ConcurrentDictionary<string, UserConnection> _connections =new();
    public ConcurrentDictionary<string, UserConnection> connections => _connections;
}