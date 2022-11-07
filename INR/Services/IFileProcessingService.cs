namespace INR.Services
{
    public interface IFileProcessingService
    {
        Task StartProcessing(string directory);
        Task StartProcessing(List<string> fileNames);

    }
}
