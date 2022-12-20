using Microsoft.AspNetCore.Mvc;

namespace LineBotPractice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; //DI相依性注入
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //EventId eventId = new EventId(1234,"我的紀錄資訊");
            //_logger.LogWarning(eventId, "Logging紀錄資訊- Home/Index被呼叫");

            _logger.LogWarning(1234, "Logging紀錄資訊- Home/Index被呼叫");
            return View();
        }
    }
}
