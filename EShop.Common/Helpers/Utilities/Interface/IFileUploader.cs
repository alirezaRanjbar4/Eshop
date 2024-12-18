using Eshop.Common.Helpers.Utilities.Utilities.Providers;
using Microsoft.AspNetCore.Http;

namespace Eshop.Common.Helpers.Utilities.Interface
{
    public interface IFileUploader
    {
        string Upload(IFormFile file, string path);
        void UploadFile(IFormFile file, string path);
        string ReturnFileName(IFormFile file);
        UplodedFileInfo FolderingInRegisterationUnit(long OrderId, string CreationDate, string CustomerEnglishName, string OrderNameInEnglish, string FileName, long PreviousOrderId);
        UplodedFileInfo Foldering(long OrderId, string StatusKey, string CreationDate, string CustomerEnglishName, string OrderNameInEnglish, string FileName, long PreviousOrderId, long MaxResubmitionNumber);
        void CopyFile(string SourceFile, UplodedFileInfo fileInfo);
    }
}
