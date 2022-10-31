namespace INR.Models
{
    public class FileNameParts
    {
        public int TaskId { get; set; }
        public string PatientCode { get; set; }
        public int CameraId { get; set; }

        public int HandId { get; set; }
        public string HandType { get; set; }
        public bool IsImpaired {
            get {
                if (HandType == "Impaired")
                    return true;
                return false;
            }
        }

    }
}
