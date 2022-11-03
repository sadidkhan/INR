using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;

namespace INR.DAL.Repositories
{
    public class TaskSegmentHandCameraMappingRepository : GenericRepository<TaskSegmentHandCameraMapping>, ITaskSegmentHandCameraMappingRepository
    {
        public TaskSegmentHandCameraMappingRepository(InrDbContext dbContext) : base(dbContext)
        {
        }
    }
}
