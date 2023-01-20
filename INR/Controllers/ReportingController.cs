using INR.DAL.Repositories.Interfaces;
using INR.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INR.Models;
using INR.DAL.Models;
using System;
using System.Collections;

namespace INR.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<dynamic>> GetCameraChangeReport()
        {
            var grp1 = await GetGroupWiseCameraChangeReport(1);
            var grp2 = await GetGroupWiseCameraChangeReport(2);
            var grp3 = await GetGroupWiseCameraChangeReport(3);
            var grp4 = await GetGroupWiseCameraChangeReport(4);
            var result = grp1.Union(grp2).Union(grp3).Union(grp4).OrderBy(i => i.GroupId).ToList();
            return Ok(result);

        }

        private async Task<List<GroupSegmentCameraSwitchCountModel>> GetGroupWiseCameraChangeReport(int grpId = 1)
        {
            var grp = GetGroupedData(grpId).Select(i => new
            {
                PatientId = i.PatientId,
                TaskId = i.TaskId,
                HandId = i.HandId,
                SegmentId = i.SegmentId,
                CameraId = i.CameraId,
                ViewType = i.ViewType
            });

            var r = await (from item in grp
                           group item by new { item.TaskId, item.HandId, item.PatientId, item.SegmentId } into g
                           select new
                           {
                               TaskId = g.Key.TaskId,
                               HandId = g.Key.HandId,
                               PatientId = g.Key.PatientId,
                               SegmentId = g.Key.SegmentId,
                               Items = g.ToList()
                           }).ToListAsync();



            var segmentCameraSwitchDict = new Dictionary<int, Dictionary<string, int>>();
            foreach (var item in r)
            {
                var viewTypeDict = new Dictionary<string, int>() { 
                    { "Ipsilateral", 0 }, { "Contralateral", 0 }, { "Transverse", 0 }, { "Back", 0 } 
                };

                if (!segmentCameraSwitchDict.ContainsKey(item.SegmentId))
                {
                    segmentCameraSwitchDict.Add(item.SegmentId, viewTypeDict);
                }

                var cameraSwitch = 0;
                var prevTask = -1;
                var prevCamera = -1;
                var prevCameraName = string.Empty;
                foreach (var i in item.Items)
                {
                    if (prevTask == -1 || prevTask != i.TaskId)
                    {
                        prevTask = i.TaskId;
                    }

                    if (prevCamera == -1)
                    {
                        prevCamera = i.CameraId;
                        prevCameraName = i.ViewType;
                        continue;
                    }

                    if (prevCamera == i.CameraId)
                        continue;
                    else
                    {
                        var seg = segmentCameraSwitchDict[item.SegmentId];
                        seg[i.ViewType] = seg[i.ViewType] + 1;
                        cameraSwitch++;
                        prevCamera = i.CameraId;
                        prevCameraName = i.ViewType;
                    }
                }

            }

            var list = segmentCameraSwitchDict.Select(x => new GroupSegmentCameraSwitchCountModel
            {
                GroupId = grpId,
                SegmentId = x.Key,
                Ipsilateral = x.Value["Ipsilateral"],
                Contralateral = x.Value["Contralateral"],
                Transverse = x.Value["Transverse"],
                Back = x.Value["Back"],
                CameraSwitchCount = x.Value["Ipsilateral"] + x.Value["Contralateral"] + x.Value["Transverse"] + x.Value["Back"]
            }).ToList();
            return list;

        }

        private IQueryable<VideoSegmentationHistory> GetGroupedData(int group = 1)
        {
            int startTaskId = 1, endTaskId = 6;
            if (group == 2)
            {
                startTaskId = 7;
                endTaskId = 10;
            }
            else if (group == 3)
            {
                startTaskId = 11;
                endTaskId = 16;
            }
            else if (group == 4)
            {
                startTaskId = 17;
                endTaskId = 19;
            }

            var grp = _unitOfWork.Repository<IVideoSegmentationHistoryRepository>().GetQuery()
                .Where(v => v.TaskId >= startTaskId && v.TaskId <= endTaskId);

            return grp;
        }


        [HttpGet]
        public async Task<ActionResult<dynamic>> GetSegmentViewTypePercentage()
        {
            var grp1 = GetSegmentationReport(1);
            var grp2 = GetSegmentationReport(2);
            var grp3 = GetSegmentationReport(3);
            var grp4 = GetSegmentationReport(4);

            var result = await grp1.Union(grp2).Union(grp3).Union(grp4).OrderBy(i => i.Group).ToListAsync();

            return Ok(result);
        }

        private IQueryable<GroupSegmentPercentageModel> GetSegmentationReport(int grpId = 1)
        {
            var grp = GetGroupedData(grpId);
            var viewTypeCountPerSegment = (from item in grp
                                           group item by new { item.SegmentId, item.SegmentName, item.ViewType } into g
                                           select new
                                           {
                                               segmentId = g.Key.SegmentId,
                                               segmentName = g.Key.SegmentName,
                                               viewType = g.Key.ViewType,
                                               CountPerSegment = g.Count()
                                           });

            var segmentCount = (from item in grp
                                group item by item.SegmentId into g
                                select new
                                {
                                    segmentId = g.Key,
                                    Count = g.Count()
                                });

            var resultOfGrp1 = (from vc in viewTypeCountPerSegment
                                join sc in segmentCount on vc.segmentId equals sc.segmentId
                                select new GroupSegmentPercentageModel
                                {
                                    SegmentId = vc.segmentId,
                                    SegmentName = vc.segmentName,
                                    ViewType = vc.viewType,
                                    Group = grpId,
                                    ViewTypeCountPerSegment = vc.CountPerSegment,
                                    Percentage = (vc.CountPerSegment * 100) / sc.Count
                                });

            return resultOfGrp1;
        }
    }
}
