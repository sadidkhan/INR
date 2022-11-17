using System.ComponentModel.DataAnnotations.Schema;

namespace INR.DAL.Models
{
    [Table("TaskSegmentHandCameraMapping")]
    public class TaskSegmentHandCameraMapping
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int SegmentId { get; set; }
        public int HandId { get; set; }
        public int CameraId { get; set; } // determine if
        public string ViewType { get; set; }
        public string Definition { get; set; }

        public virtual Segment Segment { get; set; }
        public virtual Camera Camera { get; set; }

    }
}
