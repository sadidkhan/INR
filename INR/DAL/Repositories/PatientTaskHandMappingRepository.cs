using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace INR.DAL.Repositories
{
    public class PatientTaskHandMappingRepository : GenericRepository<PatientTaskHandMapping>, IPatientTaskHandMappingRepository
    {
        public PatientTaskHandMappingRepository(InrDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PatientTaskHandMapping?> GetPTHMapping(int patientId, int taskId, int handId)
        {
            var item = await GetQuery().Where(p => p.PatientId == patientId
            && p.TaskId == taskId
            && p.HandId == handId).SingleOrDefaultAsync();

            return item;
        }
    }
}
