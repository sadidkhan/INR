using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;

namespace INR.DAL.Repositories
{
    public class TaskSegmentCameraMappingRepository : GenericRepository<TaskSegmentHandCameraMapping>, ITaskSegmentCameraMappingRepository
    {
        public TaskSegmentCameraMappingRepository(InrDbContext dbContext) : base(dbContext)
        {
        }
    }
}
