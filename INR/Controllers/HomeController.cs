using INR.Services;
using Microsoft.AspNetCore.Mvc;

namespace INR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IFileProcessingService _fileProcessingService;

        public HomeController(IFileProcessingService fileProcessingService) {
            _fileProcessingService = fileProcessingService;
        }

        [HttpGet(Name = "StartFileProcessing")]
        public async Task StartFileProcessing()
        {
            var dir = "E:\\project\\INR-Tamim\\Videos";
            await _fileProcessingService.StartProcessing(dir);
            var a = 5;
        }


    }
}
