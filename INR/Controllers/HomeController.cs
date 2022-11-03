using INR.Services;
using Microsoft.AspNetCore.Mvc;

namespace INR.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IFileProcessingService _fileProcessingService;
        private readonly IConfiguration _configuration;

        public HomeController(IFileProcessingService fileProcessingService, IConfiguration configuration) {
            _fileProcessingService = fileProcessingService;
            _configuration = configuration;
        }


        [HttpGet]
        public async Task StartFileProcessing()
        {
            var dir = _configuration["FileDirectory"];// "E:\\project\\INR-Tamim\\Videos";
            await _fileProcessingService.StartProcessing(dir);
            var a = 5;
        }
    }
}
