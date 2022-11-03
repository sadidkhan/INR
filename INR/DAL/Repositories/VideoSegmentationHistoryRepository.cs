using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;

namespace INR.DAL.Repositories
{
    public class VideoSegmentationHistoryRepository : GenericRepository<VideoSegmentationHistory>, IVideoSegmentationHistoryRepository
    {
        public VideoSegmentationHistoryRepository(InrDbContext dbContext) : base(dbContext)
        {
        }
    }
}
