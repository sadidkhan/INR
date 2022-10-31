using INR.DAL.Repositories.Interfaces;

namespace INR.DAL
{
    public interface IUnitOfWork: IDisposable
    {
        public IPatientRepository Patients { get; }
        void SaveChanges();
        Task<int> SaveChangesAsync();
        T Repository<T>() where T : class;
    }
}
