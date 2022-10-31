using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;

namespace INR.DAL.Repositories
{
    public class CameraRepository : GenericRepository<Camera>, ICameraRepository
    {
        public CameraRepository(InrDbContext dbContext) : base(dbContext)
        {
        }
    }
}
