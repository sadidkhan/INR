using INR.DAL.Models;

namespace INR.Models.RequestModels
{
    public class RatingSubmitModel
    {
        public TaskRating Task { get; set; }
        public IList<SegmentRating> Segments { get; set; }
    }
}
