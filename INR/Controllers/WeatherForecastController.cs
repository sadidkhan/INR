//using INR.DAL;
//using INR.DAL.Models;
//using INR.DAL.Repositories.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace INR.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class WeatherForecastController : ControllerBase
//    {
//        private static readonly string[] Summaries = new[]
//        {
//        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//    };

//        private readonly ILogger<WeatherForecastController> _logger;
//        private readonly IUnitOfWork _unitOfWork;


//        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWork unitOfWork)
//        {
//            _logger = logger;
//            _unitOfWork = unitOfWork;

//        }

//        [HttpGet(Name = "GetWeatherForecast")]
//        public IEnumerable<WeatherForecast> Get()
//        {
            
//            _unitOfWork.Repository<ICameraRepository>().Add(new Camera { ViewType = "test" });
//            _unitOfWork.SaveChanges();
//            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//            {
//                Date = DateTime.Now.AddDays(index),
//                TemperatureC = Random.Shared.Next(-20, 55),
//                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//            })
//            .ToArray();
//        }

//        [HttpGet(Name = "GetPing")]
//        public string Ping()
//        {

//            return $"Server live: Request time {DateTime.Now}";
//        }
//    }
//}