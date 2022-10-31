using INR.DAL.Models;

namespace INR.DAL.Repositories.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<Patient?> GetPatient(string patientCode);
    }
}
