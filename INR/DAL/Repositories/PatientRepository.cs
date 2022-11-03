using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace INR.DAL.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(InrDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Patient?> GetPatient(string patientCode)
        {
            var item = await GetQuery().Where(p => p.PatientCode == patientCode).SingleOrDefaultAsync();
            return item;
        }
    }
}
