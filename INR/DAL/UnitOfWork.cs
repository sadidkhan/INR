using INR.DAL.Repositories;
using INR.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace INR.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InrDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;

        public IPatientRepository Patients { get; private set; }

        public UnitOfWork(InrDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
            Patients = new PatientRepository(dbContext);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public T Repository<T>() where T : class
        {
            return _serviceProvider.GetService(typeof(T)) as T;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
