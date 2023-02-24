using INR.DAL;
using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;
using INR.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INR.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RatingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("{therapistId}")]
        public async Task<ActionResult<ICollection<PthTherapistMapping>>> GetPthByTherapistId(int therapistId)
        {
            var result = await _unitOfWork.Repository<IPthTherapistMappingRepository>().GetQuery()
                .Include(s=> s.PatientTaskHandMapping).Where(s => s.TherapistId == therapistId).ToListAsync();
            return Ok(result);
        }

        [HttpGet("{pthId}")]
        public async Task<ActionResult<ICollection<SegmentFileInformation>>> GetSegmentFileInfo(int pthId)
        {
            var result = await _unitOfWork.Repository<ISegmentFileInformationRepository>().GetQuery()
                .Where(s => s.PatientTaskHandMappingId == pthId).ToListAsync();
            return Ok(result);
        }

        [HttpGet("{pthId}")]
        public async Task<ActionResult<RatingSubmitModel>> GetAlreadySubmittedRating(int pthId)
        {
            var taskRating = await _unitOfWork.Repository<ITaskRatingRepository>().GetQuery()
                .Where(t => t.PatientTaskHandMappingId == pthId).SingleOrDefaultAsync();

            var segmentRatings = await _unitOfWork.Repository<ISegmentRatingRepository>().GetQuery()
                .Where(s => s.PatientTaskHandMappingId == pthId).ToListAsync();

            var result = new RatingSubmitModel
            {
                Task = taskRating,
                Segments = segmentRatings
            };
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRating(RatingSubmitModel model)
        {
            if (model.Task.Id > 0)
            {
                _unitOfWork.Repository<ITaskRatingRepository>().UpdateEntity(model.Task);
            }
            else { 
                await _unitOfWork.Repository<ITaskRatingRepository>().AddAsync(model.Task);
            }

            foreach(var segmentRating in model.Segments)
            {
                if (segmentRating.Id > 0)
                {
                    _unitOfWork.Repository<ISegmentRatingRepository>().UpdateEntity(segmentRating);
                }
                else {
                    await _unitOfWork.Repository<ISegmentRatingRepository>().AddAsync(segmentRating);
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return Ok("Success");
        }
    }
}
