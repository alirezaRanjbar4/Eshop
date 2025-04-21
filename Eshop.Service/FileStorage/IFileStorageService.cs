using Eshop.Common.Helpers.Utilities.Utilities.Providers;
using Microsoft.AspNetCore.Http;

namespace Eshop.Service.FileStorage
{
    public interface IFileStorageService
    {
        Task DeleteFile(string fileRoute, string fileName);
        Task<string> SaveFile(string containerName, IFormFile file);
        Task<string> SaveFile(string containerName, byte[] file);
        Task<string> EditFile(string containerName, IFormFile file, string fileRoute);
        void CopyFile(string SourceFile, UplodedFileInfo fileInfo);
        Task<string> Upload(IFormFile file, string path);
        void UploadFile(IFormFile file, string path);
        void UploadFile(IFormFile file, string path, string fileName);
        void CombineAndSaveFile(List<IFormFile> fileSlices, string outputPath, string fileName);
        UplodedFileInfo FolderingInRegisterationUnit(string OrderNumber, string CreationDate, string CustomerEnglishName, string OrderNameInEnglish, string FileName, string PreviousOrderId);
        string[] PathDecode(string StatusKey);
        UplodedFileInfo Foldering(string OrderNumber, string StatusKey, string CreationDate, string CustomerEnglishName, string OrderNameInEnglish, string FileName, string PreviousOrderId, int MaxResubmitionNumber);
        UplodedFileInfo FolderInOrderEditing(string directory, string FileName, int MaxResubmittingNumber);
        string ReturnFileNameAddedByTime(string FileName);
        string ReturnFileName(IFormFile file);
        string GenerateFileName();
        string GetHostUrl();
        string GetWebRootPath();
        UplodedFileInfo OrderApproveFoldering(string OrderNumber, string StatusKey, string CreationDate, string CustomerEnglishName, string OrderNameInEnglish, string FileName, string PreviousOrderId, int MaxResubmittingNumber);
        Task CreateFileImagesInTempFolder(IList<string> filePaths, Guid orderId);
        Task<string> SaveNamedFile(IFormFile file, string? containerName = null, string? fileName = null);
        Task<string> SaveFullPathFile(IFormFile file, string fullPath, string? fileName = null);
    }
}
