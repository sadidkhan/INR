namespace INR.DAL.Models
{
    public class SegmentRating
    {
        public int Id { get; set; }
        public int PatientTaskHandMappingId { get; set; }
        public int SegmentId { get; set; }
        public bool Completed { get; set; }
        public bool Initialized { get; set; }
        public bool Time { get; set; }
        public bool Impaired { get; set; }
        public bool SEAFR { get; set; }
        public bool TS { get; set; }
        public bool ROME { get; set; }
        public bool FPS { get; set; }
        public bool WPAT { get; set; }
        public bool HA { get; set; }
        public bool DP { get; set; }
        public bool DPO { get; set; }
        public bool SAT { get; set; }
        public bool DMR { get; set; }
        public bool THS { get; set; }
        public bool PP { get; set; }
        public string Rating { get; set; }
        public int TherapistId { get; set; }
        public virtual PatientTaskHandMapping PatientTaskHandMapping { get; set; }

    }
}
