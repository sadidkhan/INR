namespace INR.DAL.Models
{
    public class PthTherapistMapping
    {
        public int Id { get; set; }
        public int PatientTaskHandMappingId { get; set; }
        public int TherapistId { get; set; }
        public bool IsSubmitted { get; set; }
        public virtual PatientTaskHandMapping PatientTaskHandMapping { get; set; }
    }
}
