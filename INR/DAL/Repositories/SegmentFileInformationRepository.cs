using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;

namespace INR.DAL.Repositories
{
    public class SegmentFileInformationRepository : GenericRepository<SegmentFileInformation>, ISegmentFileInformationRepository
    {
        public SegmentFileInformationRepository(InrDbContext dbContext) : base(dbContext)
        {
        }
    }
}
