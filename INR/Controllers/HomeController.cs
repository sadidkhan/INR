using INR.Services;
using Microsoft.AspNetCore.Mvc;

namespace INR.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IFileProcessingService _fileProcessingService;

        public HomeController(IFileProcessingService fileProcessingService) {
            _fileProcessingService = fileProcessingService;
        }


        [HttpGet]
        public async Task StartFileProcessing()
        {
            var dir = "E:\\project\\INR-Tamim\\Videos";
            await _fileProcessingService.StartProcessing(dir);
            var a = 5;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Success");
        }

        [HttpGet]
        public IActionResult Test1()
        {
            return Ok("Success 2");
        }


    }
}
