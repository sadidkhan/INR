  select SegmentId,
    CameraId,
    HandId,
    COUNT(*) as Occurance,
    SUM(COUNT(0)) OVER(PARTITION BY SegmentId) AS [Total],
    CONVERT(DOUBLE PRECISION, ROUND((COUNT(*) * 100.0)/ SUM(COUNT(0)) OVER(PARTITION BY SegmentId), 2))  As Percentage
  from [VideoSegmentationHistory]
  --where SegmentId = 1
  group by SegmentId, CameraId, HandId



Begin tran

update [VideoSegmentationHistory]
set SegmentName = (select Name from Segment where Id = SegmentId)

update [VideoSegmentationHistory]
set HandType = Case when (select IsImpaired from [PatientTaskHandMapping] as pthm where pthm.HandId = vsh.HandId and pthm.TaskId = vsh.TaskId and pthm.PatientId = vsh.PatientId) = 1 then 'Impaired' else 'Unimpaired' end
from [VideoSegmentationHistory] as vsh


update [VideoSegmentationHistory]
set ViewType = Case when HandId = 1 then 'Contralateral' else 'Ipsilateral' END
where CameraId = 1

update [VideoSegmentationHistory]
set ViewType = Case when HandId = 1 then 'Ipsilateral' else 'Contralateral' END
where CameraId = 4

update [VideoSegmentationHistory]
set ViewType = 'Back'
where CameraId = 2

update [VideoSegmentationHistory]
set ViewType = 'Transverse'
where ViewType is null

Rollback


-- viewtype wise percentage
select SegmentId, segmentName, viewType, Count(*) as occurance, Round((Count(*) * 100.00) / Sum(count(0)) over(partition by segmentId), 2) as Percentage from VideoSegmentationHistory
group by segmentId, segmentName, viewType

-- end
