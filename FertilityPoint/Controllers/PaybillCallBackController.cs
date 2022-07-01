
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Threading.Tasks;

namespace FertilityPoint.Controllers
{
    public class PaybillCallBackController : Controller
    {

        private readonly ILogger<PaybillCallBackController> _logger;

        public PaybillCallBackController(ILogger<PaybillCallBackController> logger)
        {
            _logger = logger;           
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
