using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;

namespace INR.DAL.Repositories
{
    public class SegmentRepository : GenericRepository<Segment>, ISegmentRepository
    {
        public SegmentRepository(InrDbContext dbContext) : base(dbContext)
        {
        }
    }
}
