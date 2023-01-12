using System.ComponentModel.DataAnnotations.Schema;

namespace INR.DAL.Models
{
    [Table("VideoSegmentationHistory")]
    public class VideoSegmentationHistory
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int HandId { get; set; }
        public string HandType { get; set; }
        public int TaskId { get; set; }
        public int SegmentId { get; set; }
        public string SegmentName { get; set; }
        public int CameraId { get; set; }
        public string ViewType { get; set; }
        public int In { get; set; }
        public int Out { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsSubmitted { get; set; }
        public string Reason { get; set; }
    }
}
