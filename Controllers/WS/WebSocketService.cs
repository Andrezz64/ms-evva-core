using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace ms_evva_core.Controllers.WS
{
    public class WebSocketService
    {
        // Todo: Transfer this dictionarys to new classes in the future. In a folder called "Cache"
        private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, WebSocket>> _rooms = new();
        private static readonly ConcurrentDictionary<string, string> _hostRooms = new(); 
        private static readonly ConcurrentDictionary<string, string> _userRooms = new();

        public void AddToRoom(string roomId, string connectionId, WebSocket socket) 
        {
            var room = _rooms.GetOrAdd(roomId, _ => new ConcurrentDictionary<string, WebSocket>());
            room.TryAdd(connectionId, socket);
            _userRooms.TryAdd(connectionId, roomId);
        }


        public void RemoveFromRoom(string connectionId)
        {
            if (_userRooms.TryRemove(connectionId, out string roomId))
            {
                if (_rooms.TryGetValue(roomId, out var room))
                {
                    room.TryRemove(connectionId, out _);
                    if (room.IsEmpty)
                    {
                        _rooms.TryRemove(roomId, out _);
                    }
                }
            }
        }

        public async Task BroadcastToRoom(string roomId, string message)
        {
            if (_rooms.TryGetValue(roomId, out var room))
            {
                var tasks = room.Select(async kvp =>
                {
                    if (kvp.Value.State == WebSocketState.Open)
                    {
                        var buffer = Encoding.UTF8.GetBytes(message);
                        await kvp.Value.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                });

                await Task.WhenAll(tasks);
            }
        }

        public async Task SendToUser(string connectionId, string message)
        {
            if (_userRooms.TryGetValue(connectionId, out string roomId) &&
                _rooms.TryGetValue(roomId, out var room) &&
                room.TryGetValue(connectionId, out var socket) &&
                socket.State == WebSocketState.Open)
            {
                var buffer = Encoding.UTF8.GetBytes(message);
                await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public string GenerateConnectionId()
        {
            return Guid.NewGuid().ToString();
        }
    }
} 