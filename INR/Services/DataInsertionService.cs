namespace INR.Services
{
    public class DataInsertionService : IDataInsertionService
    {
        private readonly IFileProcessingService _fileProcessingService;

        public DataInsertionService(IFileProcessingService fileProcessingService) {
            _fileProcessingService = fileProcessingService;
        }
        public void InsertData()
        {
        }
    }
}
