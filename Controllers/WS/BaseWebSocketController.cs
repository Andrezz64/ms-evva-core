using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using ms_evva_core.Security;

namespace ms_evva_core.Controllers.WS
{
    public abstract class BaseWebSocketController : ControllerBase
    {
        protected readonly WebSocketService _webSocketService;
        protected readonly ILogger<BaseWebSocketController> _logger;

        protected BaseWebSocketController(WebSocketService webSocketService, ILogger<BaseWebSocketController> logger)
        {
            _webSocketService = webSocketService;
            _logger = logger;
        }

        protected abstract string GetRoomId();

        [Route("/ws")]
        public async Task HandleWebSocket([FromQuery] string hash = "")
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                var connectionId = _webSocketService.GenerateConnectionId();
                string roomId = string.IsNullOrEmpty(hash) ? Guid.NewGuid().ToString() : hash;

                if (!string.IsNullOrEmpty(hash) && !Encryption.ValidateHash(roomId, hash))
                {
                    _logger.LogWarning($"Tentativa de conexão com hash inválido: {hash}");
                    await webSocket.CloseAsync(WebSocketCloseStatus.PolicyViolation, "Hash inválido", CancellationToken.None);
                    return;
                }

                _webSocketService.AddToRoom(roomId, connectionId, webSocket);
                _logger.LogInformation($"Cliente {connectionId} conectado à sala {roomId}");

                try
                {
                    await HandleWebSocketConnection(webSocket, connectionId, roomId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Erro ao processar conexão WebSocket: {ex.Message}");
                }
                finally
                {
                    _webSocketService.RemoveFromRoom(connectionId);
                    _logger.LogInformation($"Cliente {connectionId} desconectado da sala {roomId}");
                }
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        protected virtual async Task HandleWebSocketConnection(WebSocket webSocket, string connectionId, string roomId)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!receiveResult.CloseStatus.HasValue)
            {
                if (receiveResult.MessageType == WebSocketMessageType.Text)
                {
                    var encryptedMessage = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
                    try
                    {
                        var decryptedMessage = Encryption.Decrypt(encryptedMessage);
                        await ProcessMessage(decryptedMessage, connectionId, roomId);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Erro ao processar mensagem criptografada: {ex.Message}");
                        await webSocket.SendAsync(
                            new ArraySegment<byte>(Encoding.UTF8.GetBytes("Erro ao processar mensagem")),
                            WebSocketMessageType.Text,
                            true,
                            CancellationToken.None);
                    }
                }

                receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);
        }

        protected virtual async Task ProcessMessage(string message, string connectionId, string roomId)
        {
            var encryptedMessage = Encryption.Encrypt(message);
            await _webSocketService.BroadcastToRoom(roomId, encryptedMessage);
        }
    }
} 