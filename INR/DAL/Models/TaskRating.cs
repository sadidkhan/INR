namespace INR.DAL.Models
{
    //public class TaskRatingCommonAttr 
    //{
    //    public int Id { get; set; }
    //    public int PatientTaskHandMappingId { get; set; }
    //    public bool Completed { get; set; }
    //    public bool Initialized { get; set; }
    //    public bool Time { get; set; }
    //    public bool Impaired { get; set; }
    //    public string Rating { get; set; }
    //    public int TherapistId { get; set; }
    //}

    public class TaskRating
    {
        public int Id { get; set; }
        public int PatientTaskHandMappingId { get; set; }
        public bool Completed { get; set; }
        public bool Initialized { get; set; }
        public bool Time { get; set; }
        public bool Impaired { get; set; }
        public bool Finger { get; set; }
        public string Rating { get; set; }
        public int TherapistId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public virtual PatientTaskHandMapping PatientTaskHandMapping { get; set; }
    }

    public class TaskRatingHistory
    {
        public int Id { get; set; }
        public int PatientTaskHandMappingId { get; set; }
        public bool Completed { get; set; }
        public bool Initialized { get; set; }
        public bool Time { get; set; }
        public bool Impaired { get; set; }
        public string Rating { get; set; }
        public int TherapistId { get; set; }
        public int CameraId { get; set; }
    }
}
