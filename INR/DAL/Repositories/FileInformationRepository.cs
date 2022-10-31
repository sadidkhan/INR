using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace INR.DAL.Repositories
{
    public class FileInformationRepository : GenericRepository<FileInformation>, IFileInformationRepository
    {
        public FileInformationRepository(InrDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<FileInformation> AddIfNotExists(FileInformation fileInformation)
        {
            var fileInfo = GetAll().Where(fi => fi.FileName == fileInformation.FileName).SingleOrDefault();
            if (fileInfo != null)
                return fileInfo;

            await AddAsync(fileInformation);
            return fileInformation;
        }

        public async Task<bool> CheckIfExists(string fileName)
        {
            var isExisting = await GetAll().AnyAsync(fi => fi.FileName == fileName);
            return isExisting;
        }
    }
}
