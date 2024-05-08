using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_api.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //1. Strongly coupled/tight coupled
        //private readonly IMyLogger _myLogger;

        //public DemoController()
        //{
        //    _myLogger = new LogToFile();
        //}

        //[HttpGet]
        //public ActionResult Index()
        //{
        //    _myLogger.Log("Index method started");
        //    return Ok();
        //}


        //2. Loosely coupled
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            //_logger.LogTrace("Log message from trace method");
            //_logger.LogDebug("Log message from Debug method");
            _logger.LogInformation("This is the home demo page");
            //_logger.LogWarning("Log message from Warning method");
            //_logger.LogError("Log message from Error method");
            //_logger.LogCritical ("Log message from Critical method");

            return Ok();
        }
    }
}

