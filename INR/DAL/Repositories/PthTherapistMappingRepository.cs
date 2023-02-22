using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;

namespace INR.DAL.Repositories
{
    public class PthTherapistMappingRepository : GenericRepository<PthTherapistMapping>, IPthTherapistMappingRepository
    {
        public PthTherapistMappingRepository(InrDbContext dbContext) : base(dbContext)
        {
        }
    }
}
