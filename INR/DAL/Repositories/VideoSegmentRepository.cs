using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;

namespace INR.DAL.Repositories
{
    public class VideoSegmentRepository : GenericRepository<VideoSegment>, IVideoSegmentRepository
    {
        public VideoSegmentRepository(InrDbContext dbContext) : base(dbContext)
        {
        }
    }
}
