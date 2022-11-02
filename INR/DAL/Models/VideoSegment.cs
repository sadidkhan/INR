using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INR.DAL.Models
{
    [Table("VideoSegment")]
    public class VideoSegment
    {
        public int Id { get; set; }
        public int PatientTaskHandMappingId { get; set; }
        public int SegmentId { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual PatientTaskHandMapping PatientTaskHandMapping { get; set; }
        public virtual Segment Segment { get; set; }
    }
}
