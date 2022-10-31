using System.ComponentModel.DataAnnotations.Schema;

namespace INR.DAL.Models
{
    [Table("Patient")]
    public class Patient
    {
        public int Id { get; set; }
        public string PatientCode { get; set; }
    }
}
