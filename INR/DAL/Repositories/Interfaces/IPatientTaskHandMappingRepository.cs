using INR.DAL.Models;

namespace INR.DAL.Repositories.Interfaces
{
    public interface IPatientTaskHandMappingRepository : IRepository<PatientTaskHandMapping>
    {
        Task<PatientTaskHandMapping?> GetPTHMapping(int patientId, int taskId, int handId);
    }
}
