using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;

namespace INR.DAL.Repositories
{
    public class CameraViewTypeRepository : GenericRepository<CameraViewType>, ICameraViewTypeRepository
    {
        public CameraViewTypeRepository(InrDbContext dbContext) : base(dbContext)
        {
        }
    }
}
