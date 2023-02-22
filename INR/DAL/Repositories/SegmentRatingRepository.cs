using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;

namespace INR.DAL.Repositories
{
    public class SegmentRatingRepository : GenericRepository<SegmentRating>, ISegmentRatingRepository
    {
        public SegmentRatingRepository(InrDbContext dbContext) : base(dbContext)
        {
        }
    }
}
