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
    public class SegmentationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SegmentationController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<PatientTaskHandMapping>>> GetPatientTaskInformation()
        {
            var result = _unitOfWork.Repository<IPatientTaskHandMappingRepository>().GetQuery().ToList();
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

                if(model.SegmentHistories.Count > 0)
                {
                    var vsh = model.SegmentHistories.First();
                    var pth = await _unitOfWork.Repository<IPatientTaskHandMappingRepository>()
                        .GetQuery().Where(i => i.PatientId == vsh.PatientId && i.TaskId == vsh.TaskId && i.HandId == vsh.HandId)
                        .FirstOrDefaultAsync();

                    var segmentsDict = await _unitOfWork.Repository<ISegmentRepository>().GetQuery().ToDictionaryAsync(s => s.Id, s => s.Name);
                    var segmentHistories = model.SegmentHistories.Select(sh => new VideoSegmentationHistory
                    {
                        PatientTaskHandMappingId = pth.Id,
                        PatientId = sh.PatientId,
                        TaskId = sh.TaskId,
                        HandId = sh.HandId,
                        SegmentId = sh.SegmentId,
                        SegmentName = segmentsDict[sh.SegmentId],
                        CameraId = sh.CameraId,
                        ViewType = GetCameraViewType(sh),
                        In = sh.Start,
                        Out = sh.End,
                        CreatedAt = sh.CreatedAt ?? DateTime.UtcNow,
                        IsSubmitted = sh.IsSubmitted,
                        Reason = sh.Reason

                    }).ToList();

                    await _unitOfWork.Repository<IVideoSegmentationHistoryRepository>().AddRangeAsync(segmentHistories);
                }
                

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

                if (model.SubmittedSegments.Count > 0) {
                    var pth = await _unitOfWork.Repository<IPatientTaskHandMappingRepository>().GetAsync(model.SubmittedSegments.First().PatientTaskHandMappingId);
                    pth.IsSubmitted = true;
                    _unitOfWork.SaveChanges();
                }
                
                return Ok();
            }
            catch(Exception ex) {
                throw ex;
            }
            
        }

        private string GetCameraViewType(SegmentState sh) {
            //if handId = 1, cam 4 ipselateral, cam 1 contalateral
            //  if handid = 2, cam 1 conta, cam 4 ipse
            //  cam3 transverse
            //  cam2 back
            string view;
            if (sh.CameraId == 1) view = (sh.HandId == 1) ? "Contralateral" : "Ipsilateral";
            else if (sh.CameraId == 4) view = (sh.HandId == 1) ? "Ipsilateral" : "Contralateral";
            else if (sh.CameraId == 2) view = "Back";
            else view = "Transverse";

            return view;
        }
    }
}
