using INR.DAL;
using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;
using INR.Models.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ICollection<PatientTaskHandMapping>> GetPatientTaskInformation()
        {
            var result = await _unitOfWork.Repository<IPatientTaskHandMappingRepository>().GetQuery().ToListAsync();
            return result;
        }

        [HttpGet("{pthId}")]
        public async Task<dynamic> GetFiles(int pthId)
        {
            var pth = await _unitOfWork.Repository<IPatientTaskHandMappingRepository>().GetQuery().Include(_ => _.Patient).Where(_ => _.Id == pthId).SingleOrDefaultAsync();

            var files = await _unitOfWork.Repository<IFileInformationRepository>()
                .GetQuery().Where(f => f.PatientTaskHandmappingId == pth.Id)
                .ToListAsync();

            var taskSegmentHandCameraMapping = await _unitOfWork.Repository<ITaskSegmentHandCameraMappingRepository>()
                .GetQuery().Where(tshcm => tshcm.TaskId == pth.TaskId && tshcm.HandId == pth.HandId).Include(_=> _.Segment).ToListAsync();

            return new { pth, files, taskSegmentHandCameraMapping }; 
        }

        [HttpPost]
        public async Task PostSegments(SegmentationPostModel model)
        {
            try
            {
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

                var submittedSegments = model.SubmittedSegments.Select(ss => new VideoSegment
                {
                    SegmentId = ss.SegmentId,
                    PatientTaskHandMappingId = ss.PatientTaskHandMappingId,
                    Start = ss.Start,
                    End = ss.End,
                    CreatedAt = DateTime.UtcNow
                }).ToList();

                await _unitOfWork.Repository<IVideoSegmentRepository>().AddRangeAsync(submittedSegments);

                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex) {
                var a = 5;
            }
            
        }
    }
}
