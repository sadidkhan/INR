using INR.DAL.Models;

namespace INR.DAL.Repositories.Interfaces
{
    public interface IFileInformationRepository : IRepository<FileInformation>
    {
        Task<bool> CheckIfExists(string fileName);
        Task<FileInformation> AddIfNotExists(FileInformation fileInformation);
    }
}
