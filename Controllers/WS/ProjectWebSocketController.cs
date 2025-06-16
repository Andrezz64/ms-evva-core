using Microsoft.AspNetCore.Mvc;

namespace ms_evva_core.Controllers.WS
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectWebSocketController : BaseWebSocketController
    {
        public ProjectWebSocketController(WebSocketService webSocketService, ILogger<ProjectWebSocketController> logger)
            : base(webSocketService, logger)
        {
        }

        protected override string GetRoomId()
        {
            return "projects";
        }

        protected override async Task ProcessMessage(string message, string connectionId, string roomId)
        {
            _logger.LogInformation($"Mensagem recebida na sala {roomId} do cliente {connectionId}: {message}");
            await base.ProcessMessage(message, connectionId, roomId);
        }
    }
} 