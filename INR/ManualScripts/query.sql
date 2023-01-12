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

Alter table [VideoSegmentationHistory]
add ViewType NVARCHAR(50) 

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
