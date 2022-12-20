using Line.Messaging;
using LineBotPractice.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LineBotPractice.Controllers
{
    [Route("api/linebot")]
    public class LineBotController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext? _httpContext;
        private readonly LineBotConfig _lineBotConfig;
        private readonly ILogger<LineBotController> _logger; //DI相依性注入

        public LineBotController(IServiceProvider serviceProvider, LineBotConfig lineBotConfig, ILogger<LineBotController> logger)
        {
            _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            _httpContext = _httpContextAccessor.HttpContext;
            _lineBotConfig = lineBotConfig;
            _logger = logger;
        }

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        [Route("run")]
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            try
            {
                //建立thread非同步請求
                if (_httpContext != null)
                {
                    if (string.IsNullOrEmpty(_lineBotConfig.AccessToken) || string.IsNullOrEmpty(_lineBotConfig.ChannelSecret))
                    {
                        throw new Exception("AccessToken or ChannelSecret 不可為空");
                    }
                    var events = await _httpContext.Request.GetWebhookEventsAsync(_lineBotConfig.ChannelSecret);
                    var lineMessagingClient = new LineMessagingClient(_lineBotConfig.AccessToken);

                    var lineBotApp = new LineBotApp(lineMessagingClient);
                    await lineBotApp.RunAsync(events);
                }
            }
            catch (Exception ex)
            {
                //需要 Log 可自行加入
                _logger.LogError(JsonConvert.SerializeObject(ex));
            }
            return Ok();
        }
    }
}
