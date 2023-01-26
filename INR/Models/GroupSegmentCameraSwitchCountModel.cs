namespace INR.Models
{
    public class GroupSegmentCameraSwitchCountModel
    {
        public int GroupId { get; set; }
        public int SegmentId { get; set; }
        public int CameraSwitchCount { get; set; }
        public int Ipsilateral { get; set; }
        public int Contralateral { get; set; }
        public int Transverse { get; set; }
        public int Back { get; set; }

    }
}
