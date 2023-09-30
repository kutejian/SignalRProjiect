using Microsoft.AspNetCore.Mvc;
using WebApplication1.Chat;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IConnectChat _connectChat;

        public WeatherForecastController(IConnectChat connectChat)
        {
            _connectChat = connectChat;
        }

        [HttpGet(Name = "ConnectChat")]
        public int ConnectChat()
        {
            _connectChat.CallByClient("ssss");
            return 0;
        }
    }
}