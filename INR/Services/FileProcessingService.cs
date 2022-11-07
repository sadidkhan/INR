using INR.DAL;
using INR.DAL.Models;
using INR.DAL.Repositories.Interfaces;
using Microsoft.OpenApi.Any;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace INR.Services
{
    public enum HandEnum { 
        left = 1,
        right,
    }

    public class FileProcessingService : IFileProcessingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FileProcessingService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        private (string, string, int, int, string, int) Parse(string fileNameIncludingPath)
        {
            // example: E:/Videos/ARAT_029_left_Unimpaired_cam1activity_1.webm
            // cam 1 = right, cam 2 back, cam 3 = top, cam 4 = left

            var segments = fileNameIncludingPath.Split("ARAT_");
            var path = segments[0];
            var fileName = "ARAT_" + segments[1]; // ARAT_ was removed from name during split, so adding it again

            var fileNameSegments = fileName.Split('.');
            var fileInfo = fileNameSegments[0];
            var extenstion = fileNameSegments[1];

            var parts = fileInfo.Split('_');
            var type = parts[0];
            var patientCode = parts[1];
            var hand = parts[2];
            var handType = parts[3];
            var camera = parts[4].Split("activity")[0].Split("cam")[1];
            var task = parts[5];

            var handId = (int)Enum.Parse(typeof(HandEnum), hand);
            var cameraId = Int32.Parse(camera);
            var taskId = Int32.Parse(task);

            return (fileName, patientCode, taskId, handId, handType, cameraId);
        }

        private async Task<Patient> GetPatient(string patientCode) {
            var patient = await _unitOfWork.Repository<IPatientRepository>().GetPatient(patientCode);
            if (patient is null) {
                patient = new Patient { PatientCode = patientCode };
                await _unitOfWork.Repository<IPatientRepository>().AddAsync(patient);
                await _unitOfWork.SaveChangesAsync();
            }
            return patient;
        }

        private async Task<PatientTaskHandMapping> GetPatientTaskHandMapping(int patientId, int taskId, int handId, string handType)
        {
            var pthMappingEntity = await _unitOfWork.Repository<IPatientTaskHandMappingRepository>().GetPTHMapping(patientId, taskId, handId);
            if (pthMappingEntity is null)
            {
                pthMappingEntity = new PatientTaskHandMapping
                {
                    PatientId = patientId,
                    HandId = handId,
                    TaskId = taskId,
                    IsImpaired = (handType is "Impaired" or "impaired") ? true : false
                };
                await _unitOfWork.Repository<IPatientTaskHandMappingRepository>().AddAsync(pthMappingEntity);
                await _unitOfWork.SaveChangesAsync();
            }
            return pthMappingEntity;
        }

        private async Task AddFileIfNotExists(string fileName, int patientTaskHandMappingId, int cameraId)
        {
            var isExisting = await _unitOfWork.Repository<IFileInformationRepository>().CheckIfExists(fileName);
            if (isExisting)
                return;
            
            var fileInfo = new FileInformation
            {
                FileName = fileName,
                PatientTaskHandmappingId = patientTaskHandMappingId,
                CameraId = cameraId
            };
            await _unitOfWork.Repository<IFileInformationRepository>().AddIfNotExists(fileInfo);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task StartProcessing(string directory)
        {
            try
            {
                var files = Directory.EnumerateFiles(directory);

                foreach (var item in files)
                {
                    var (fileName, patientCode, taskId, handId, handType, cameraId) = Parse(item);

                    var patient = await GetPatient(patientCode);
                    var patientTaskHandMapping = await GetPatientTaskHandMapping(patient.Id, taskId, handId, handType);

                    await AddFileIfNotExists(fileName, patientTaskHandMapping.Id, cameraId);
                }
            }
            catch(Exception ex) {
                var b = 5;
            }
            
        }

        public async Task StartProcessing(List<string> fileNames)
        {
            try
            {
                foreach (var item in fileNames)
                {
                    var (fileName, patientCode, taskId, handId, handType, cameraId) = Parse(item);

                    var patient = await GetPatient(patientCode);
                    var patientTaskHandMapping = await GetPatientTaskHandMapping(patient.Id, taskId, handId, handType);

                    await AddFileIfNotExists(fileName, patientTaskHandMapping.Id, cameraId);
                }
            }
            catch (Exception ex)
            {
                var b = 5;
            }
        }
    }
}
