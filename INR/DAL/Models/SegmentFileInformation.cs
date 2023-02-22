namespace INR.DAL.Models
{
    public class SegmentFileInformation
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int PatientTaskHandMappingId { get; set; }
        public int CameraId { get; set; }
        public int SegmentId { get; set; }

        public virtual Segment Segment { get; set; }
        public virtual PatientTaskHandMapping PatientTaskHandMapping { get; set; }
    }
}
