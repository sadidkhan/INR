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

        [HttpGet("{pthId}/{therapistId}")]
        public async Task<ActionResult<RatingSubmitModel>> GetAlreadySubmittedRating(int pthId, int therapistId)
        {
            var taskRating = await _unitOfWork.Repository<ITaskRatingRepository>().GetQuery()
                .Where(t => t.PatientTaskHandMappingId == pthId && t.TherapistId == therapistId).SingleOrDefaultAsync();

            var segmentRatings = await _unitOfWork.Repository<ISegmentRatingRepository>().GetQuery()
                .Where(s => s.PatientTaskHandMappingId == pthId && s.TherapistId == therapistId).ToListAsync();

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
            if (model.Task == null)
            {
                return BadRequest("Submitted Task for rating can not be empty");
            }
            else if (model.Task.PatientTaskHandMappingId <= 0)
            {
                return BadRequest("Submitted Task does not have PTH id");
            }
            else if (model.Task.TherapistId <= 0)
            {
                return BadRequest("Submitted Task does not have Therapist id");
            }
            //else if (model.Segments.Count < 1) 
            //{
            //    return BadRequest("Submitted segments are empty");
            //}
            
            var pthId = model.Task.PatientTaskHandMappingId;
            var therapistId = model.Task.TherapistId;

            var submittedTask = await _unitOfWork.Repository<ITaskRatingRepository>().GetQuery().AsNoTracking()
                .Where(t => t.PatientTaskHandMappingId == pthId && t.TherapistId == therapistId).SingleOrDefaultAsync();
            if (submittedTask != null)
            {
                model.Task.Id = submittedTask.Id;
                _unitOfWork.Repository<ITaskRatingRepository>().UpdateEntity(model.Task);
            }
            else {
                await _unitOfWork.Repository<ITaskRatingRepository>().AddAsync(model.Task);
            }
            

            if (model.Segments.Count > 0) { 
                var submittedSegmentsRating = await _unitOfWork.Repository<ISegmentRatingRepository>().GetQuery().AsNoTracking()
                    .Where(t => t.PatientTaskHandMappingId == pthId && t.TherapistId == therapistId).ToListAsync();

                var submittedSegmentRatingDict = submittedSegmentsRating.ToDictionary(x => x.SegmentId, x => x);

                foreach (var segmentRating in model.Segments)
                {
                    if (submittedSegmentRatingDict.ContainsKey(segmentRating.SegmentId))
                    {
                        segmentRating.Id = submittedSegmentRatingDict[segmentRating.SegmentId].Id;
                        _unitOfWork.Repository<ISegmentRatingRepository>().UpdateEntity(segmentRating);
                    }
                    else 
                    {
                        await _unitOfWork.Repository<ISegmentRatingRepository>().AddAsync(segmentRating);
                    }
                }
            }


            var pthTherapistMapping = await _unitOfWork.Repository<IPthTherapistMappingRepository>().GetQuery()
                .Where(i => i.PatientTaskHandMappingId == pthId && i.TherapistId == therapistId).SingleOrDefaultAsync();

            if (pthTherapistMapping == null) {
                return BadRequest("No mapping found for PTH and Therapist");
            }

            if (!pthTherapistMapping.IsSubmitted) {
                pthTherapistMapping.IsSubmitted = true;
                _unitOfWork.Repository<IPthTherapistMappingRepository>().Update(pthTherapistMapping);
            }

            await _unitOfWork.SaveChangesAsync();

            return Ok("Success");
        }
    }
}
