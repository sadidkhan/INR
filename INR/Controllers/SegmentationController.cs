using INR.DAL;
using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;
using INR.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace INR.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SegmentationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SegmentationController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<PatientTaskHandMapping>>> GetPatientTaskInformation()
        {
            var result = await _unitOfWork.Repository<IPatientTaskHandMappingRepository>().GetQuery().Include(pthm => pthm.Patient).ToListAsync();
            return Ok(result);
        }

        [HttpGet("{pthId}")]
        public async Task<ActionResult<dynamic>> GetFiles(int pthId)
        {
            var pth = await _unitOfWork.Repository<IPatientTaskHandMappingRepository>().GetQuery().Include(_ => _.Patient).Where(_ => _.Id == pthId).SingleOrDefaultAsync();

            var files = await _unitOfWork.Repository<IFileInformationRepository>()
                .GetQuery().Where(f => f.PatientTaskHandmappingId == pth.Id)
                .ToListAsync();

            var taskSegmentHandCameraMapping = await _unitOfWork.Repository<ITaskSegmentHandCameraMappingRepository>()
                .GetQuery().Where(tshcm => tshcm.TaskId == pth.TaskId && tshcm.HandId == pth.HandId).Include(_=> _.Segment).ToListAsync();

            return Ok(new { pth, files, taskSegmentHandCameraMapping }); 
        }

        [HttpGet("{pthId}")]
        public async Task<ActionResult<List<VideoSegment>>> GetSegments(int pthId) {
            var segments = await _unitOfWork.Repository<IVideoSegmentRepository>().GetQuery()
                    .Where(vs => vs.PatientTaskHandMappingId == pthId).ToListAsync();

            return Ok(segments);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes. Status400BadRequest)]
        public async Task<IActionResult> PostSegments(SegmentationPostModel model)
        {
            try
            {
                if (model.SubmittedSegments.Count == 0) {
                    return BadRequest("Submitted segmentations can not be empty");
                }

                var segmentHistories = model.SegmentHistories.Select(sh => new VideoSegmentationHistory
                {
                    PatientId = sh.PatientId,
                    TaskId = sh.TaskId,
                    HandId = sh.HandId,
                    SegmentId = sh.SegmentId,
                    CameraId = sh.CameraId,
                    In = sh.Start,
                    Out = sh.End,
                    CreatedAt = sh.CreatedAt ?? DateTime.UtcNow,
                    IsSubmitted = sh.IsSubmitted
                }).ToList(); 

                await _unitOfWork.Repository<IVideoSegmentationHistoryRepository>().AddRangeAsync(segmentHistories);

                foreach (var ss in model.SubmittedSegments) {
                    if (ss.Id > 0)
                    {
                        var segment = await _unitOfWork.Repository<IVideoSegmentRepository>().GetAsync<int>(ss.Id);
                        if (segment != null)
                        {
                            segment.Start = ss.Start;
                            segment.End = ss.End;
                            segment.UpdatedAt = DateTime.UtcNow;
                        }
                    }
                    else {
                        var newVideoSegment = new VideoSegment
                        {
                            SegmentId = ss.SegmentId,
                            PatientTaskHandMappingId = ss.PatientTaskHandMappingId,
                            Start = ss.Start,
                            End = ss.End,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        };
                        await _unitOfWork.Repository<IVideoSegmentRepository>().AddAsync(newVideoSegment);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }
                //var submittedSegments = model.SubmittedSegments.Select(ss => new VideoSegment
                //{
                //    SegmentId = ss.SegmentId,
                //    PatientTaskHandMappingId = ss.PatientTaskHandMappingId,
                //    Start = ss.Start,
                //    End = ss.End,
                //    CreatedAt = DateTime.UtcNow
                //}).ToList();

                //await _unitOfWork.Repository<IVideoSegmentRepository>().AddRangeAsync(submittedSegments);

                //await _unitOfWork.SaveChangesAsync();
                return Ok();
            }
            catch(Exception ex) {
                throw ex;
            }
            
        }
    }
}
