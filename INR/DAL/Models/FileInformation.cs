using System.ComponentModel.DataAnnotations.Schema;

namespace INR.DAL.Models
{
    [Table("FileInformation")]
    public class FileInformation
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        //public string FilePath { get; set; }
        public int PatientTaskHandmappingId { get; set; }
        public int CameraId { get; set; }

        public virtual Camera Camera { get; set; }
        public virtual PatientTaskHandMapping PatientTaskHandMapping { get; set; }
    }
}
