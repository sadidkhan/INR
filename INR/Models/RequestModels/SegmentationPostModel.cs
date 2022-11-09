namespace INR.Models.RequestModels
{

    public class SegmentState {
        public int PatientId { get; set; }
        public int TaskId { get; set; }
        public int HandId { get; set; }
        public int SegmentId { get; set; }
        public int CameraId { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsSubmitted { get; set; }
    }

    public class SubmittedSegment
    {
        public int Id { get; set; }
        public int PatientTaskHandMappingId { get; set; }
        public int SegmentId { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
    }

    public class SegmentationPostModel
    {
        public List<SegmentState> SegmentHistories { get; set; }
        public List<SubmittedSegment> SubmittedSegments { get; set; }
    }
}
