using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;

namespace INR.DAL.Repositories
{
    public class TaskRatingRepository : GenericRepository<TaskRating>, ITaskRatingRepository
    {
        public TaskRatingRepository(InrDbContext dbContext) : base(dbContext)
        {
        }
    }
}
