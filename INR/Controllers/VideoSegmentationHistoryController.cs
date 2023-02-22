using INR.DAL;
using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INR.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VideoSegmentationHistoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VideoSegmentationHistoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<VideoSegmentationHistory>> GetVideoSegmentationHistoryData()
        {
            var result = await _unitOfWork.Repository<IVideoSegmentationHistoryRepository>().GetQuery().ToListAsync();
            return Ok(result);
        }
    }
}
