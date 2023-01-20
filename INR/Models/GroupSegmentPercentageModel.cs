namespace INR.Models
{
    public class GroupSegmentPercentageModel
    {
        public int SegmentId { get; set; }
        public string SegmentName { get; set; }
        public string ViewType { get; set; }
        public int Group { get; set; }
        public int ViewTypeCountPerSegment { get; set; }
        public double Percentage { get; set; }
    }
}
