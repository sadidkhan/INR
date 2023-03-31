namespace INR.DAL.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int PatientTaskHandMappingId { get; set; }
        public int? SegmentId { get; set; }
        public int TherapistId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
