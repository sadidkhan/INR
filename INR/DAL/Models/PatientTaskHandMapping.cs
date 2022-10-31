using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INR.DAL.Models
{
    [Table("PatientTaskHandMapping")]
    public class PatientTaskHandMapping
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int TaskId { get; set; }

        [Range(1, 2)]
        public int HandId { get; set; }
        public bool IsImpaired { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
