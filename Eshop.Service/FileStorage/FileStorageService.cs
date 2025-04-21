using Eshop.Common.Helpers.Utilities.Utilities.Providers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Eshop.Service.FileStorage
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FileStorageService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }


        public Task DeleteFile(string fileRoute, string fileName)
        {
            if (string.IsNullOrEmpty(fileRoute))
            {
                return Task.CompletedTask;
            }

            // var fileName = Path.GetFileName(fileRoute);
            var fileDirectory = Path.Combine(fileRoute, fileName);

            if (File.Exists(fileDirectory))
            {
                File.Delete(fileDirectory);
            }

            return Task.CompletedTask;
        }

        public async Task<string> EditFile(string containerName, IFormFile file, string fileRoute)
        {
            await DeleteFile(fileRoute, containerName);
            return await SaveFile(containerName, file);
        }

        public async Task<string> SaveFile(string containerName, IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(_webHostEnvironment.WebRootPath, containerName);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string route = Path.Combine(folder, fileName);
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var content = ms.ToArray();
                await File.WriteAllBytesAsync(route, content);
            }

            var url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var routeForDB = Path.Combine(url, containerName, fileName).Replace("\\", "/");
            return routeForDB;
        }

        public async Task<string> SaveFile(string fileName, byte[] file)
        {
            string webRootPath = $"\\\\192.168.3.235\\Rasam-Data\\Input-Customer\\SignalR\\";
            string uploadPath = Path.Combine(webRootPath, "Upload"); // مسیر ذخیره تصاویر
            // اگر پوشه "uploads" وجود نداشت، آن را ایجاد می‌کنیم
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            // مسیر کامل فایل تصویر
            string imagePath = Path.Combine(uploadPath, fileName);
            // ذخیره تصویر به عنوان یک فایل در مسیر مورد نظر
            File.WriteAllBytes(imagePath, file);
            return imagePath; // مسیر فایل ذخیره شده
        }

        public async Task<string> Upload(IFormFile file, string path)
        {
            if (file == null) return "";
            var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";

            var directoryPath = $"{_webHostEnvironment.WebRootPath}//{path}";

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var filePath = $"{directoryPath}//{fileName}";
            using var output = File.Create(filePath);
            await file.CopyToAsync(output);
            return $"{path}/{fileName}";
        }

        public string ReturnFileName(IFormFile file)
        {
            if (file == null) return "";

            var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";

            return fileName;
        }

        public string ReturnFileNameAddedByTime(string FileName)
        {

            var fileName = $"{DateTime.Now.ToFileName()}-{FileName}";

            return fileName;
        }

        public UplodedFileInfo Foldering(string OrderNumber, string StatusKey, string CreationDate, string CustomerEnglishName, string OrderNameInEnglish, string FileName, string PreviousOrderId, int MaxResubmittingNumber)
        {
            string[] Folder = PathDecode(StatusKey);
            var path = $"\\\\192.168.3.235\\Rasam-Data\\Input-Customer\\{Folder[0]}\\{CustomerEnglishName}\\{Tools.DateOnlyToFileName(CreationDate)} {OrderNameInEnglish}";

            if (!string.IsNullOrEmpty(PreviousOrderId))
                path += $"({PreviousOrderId})[{OrderNumber}]";
            else
            {
                path += $"[{OrderNumber}]";
            }

            //path += $"\\{Folder[1]}";

            if (MaxResubmittingNumber != 0) // it is resubmition file 
            { path += $"\\Seri{MaxResubmittingNumber}-{Tools.DateOnlyToFileName(DateTime.Now.ToFarsi())}"; }

            UplodedFileInfo fileInfo = new UplodedFileInfo();
            fileInfo.FileAddressInDatabase = $"{path}/{FileName}";
            //fileInfo.FileAddressInDatabase = $"{path}/{ReturnFileNameAddedByTime(FileName)}";
            fileInfo.FileDirectory = path;
            fileInfo.OrderNumber = OrderNumber;
            fileInfo.FilePureName = FileName;
            return fileInfo;
        }

        public UplodedFileInfo OrderApproveFoldering(string OrderNumber, string StatusKey, string CreationDate, string CustomerEnglishName, string OrderNameInEnglish, string FileName, string PreviousOrderId, int MaxResubmittingNumber)
        {

            string[] Folder = PathDecode(StatusKey);
            var path = $"\\\\192.168.3.235\\Rasam-Data\\Input-Customer\\{Folder[0]}\\{CustomerEnglishName}\\{Tools.DateOnlyToFileName(CreationDate)} {OrderNameInEnglish}";


            if (!string.IsNullOrEmpty(PreviousOrderId))
                path += $"({PreviousOrderId})[{OrderNumber}]";
            else
            {
                path += $"[{OrderNumber}]";
            }

            //path += $"\\{Folder[1]}";

            if (MaxResubmittingNumber != 0) // it is resubmition file 
            { path += $"\\{Folder[1]}({MaxResubmittingNumber})-{Tools.DateOnlyToFileName(DateTime.Now.ToFarsi())}"; }

            UplodedFileInfo fileInfo = new UplodedFileInfo();
            fileInfo.FileAddressInDatabase = $"{path}/{FileName}";
            //fileInfo.FileAddressInDatabase = $"{path}/{ReturnFileNameAddedByTime(FileName)}";
            fileInfo.FileDirectory = path;
            fileInfo.OrderNumber = OrderNumber;
            fileInfo.FilePureName = FileName;
            return fileInfo;
        }

        public UplodedFileInfo FolderInOrderEditing(string directory, string FileName, int MaxResubmittingNumber)
        {
            string secondToLastDirectoryName = GetSecondToLastDirectoryName(directory);

            if (MaxResubmittingNumber != 0) // it is resubmition file 
            { secondToLastDirectoryName += $"\\Seri{MaxResubmittingNumber}-{Tools.DateOnlyToFileName(DateTime.Now.ToFarsi())}"; }

            UplodedFileInfo fileInfo = new UplodedFileInfo();
            //fileInfo.FileAddressInDatabase = $"{path}/{ReturnFileNameAddedByTime(FileName)}";
            fileInfo.FileDirectory = secondToLastDirectoryName;
            // fileInfo.OrderNumber = OrderNumber;
            fileInfo.FilePureName = FileName;
            return fileInfo;
        }

        private string GetSecondToLastDirectoryName(string path)
        {
            string parentDirectory = Directory.GetParent(path).FullName;
            // string secondToLastDirectoryName = Directory.GetParent(parentDirectory).FullName;

            return parentDirectory;
        }

        public string[] PathDecode(string StatusKey)
        {
            string[] Folder = StatusKey.Split('-');
            if (Folder.Length != 2)
            {
                Array.Resize(ref Folder, 2); Folder[1] = "Input";
            }

            return Folder;
        }

        public UplodedFileInfo FolderingInRegisterationUnit(string OrderNumber, string CreationDate, string CustomerEnglishName, string OrderNameInEnglish, string FileName, string PreviousOrderId)
        {
            var path = $"RasamData//Input//{CustomerEnglishName}//{Tools.DateOnlyToFileName(CreationDate)} {OrderNameInEnglish}";

            if (!string.IsNullOrEmpty(PreviousOrderId))
                path += $"({PreviousOrderId})[{OrderNumber}]";
            else
            {
                path += $"[{OrderNumber}]";
            }

            path += "//Input";

            path += $"//{DateTime.Now.ToFileName()}";

            UplodedFileInfo fileInfo = new UplodedFileInfo();
            fileInfo.FileAddressInDatabase = $"{path}/{FileName}";
            fileInfo.FileDirectory = path;

            return fileInfo;
        }

        public void UploadFile(IFormFile file, string path)
        {
            try
            {
                if (file != null)
                {
                    string fileName = file.FileName; //  ReturnFileName(file); 
                                                     //  var directoryPath = $"{_webHostEnvironment.WebRootPath}//{path}";
                    var directoryPath = path;

                    if (!Directory.Exists(directoryPath))
                        Directory.CreateDirectory(directoryPath);

                    var filePath = $"{directoryPath}\\{fileName}";
                    using var output = File.Create(filePath);
                    file.CopyTo(output);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UploadFile(IFormFile file, string path, string fileName)
        {
            try
            {
                if (file != null)
                {

                    var directoryPath = path;

                    if (!Directory.Exists(directoryPath))
                        Directory.CreateDirectory(directoryPath);

                    var filePath = $"{directoryPath}\\{fileName}{Path.GetExtension(file.FileName).ToLowerInvariant()}";
                    using var output = File.Create(filePath);
                    file.CopyTo(output);
                }


            }
            catch (Exception)
            {

                throw;
            }

        }

        public void CopyFile(string SourceFile, UplodedFileInfo fileInfo)
        {
            string directory = $"{_webHostEnvironment.WebRootPath}//" + fileInfo.FileDirectory;
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // note : I should handle this error 

            File.Copy($"{_webHostEnvironment.WebRootPath}//{SourceFile}",
                       $"{_webHostEnvironment.WebRootPath}//{fileInfo.FileAddressInDatabase}", false);
        }

        public string GenerateFileName()
        {
            //Random FileName without Random Extension
            return Path.GetFileNameWithoutExtension(Path.GetRandomFileName());

        }

        public string GetHostUrl()
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            var host = $"{request.Scheme}://{request.Host}";
            return host;
        }

        public string GetWebRootPath()
        {
            return _webHostEnvironment.WebRootPath;
        }

        public void CombineAndSaveFile(List<IFormFile> fileSlices, string outputPath, string fileName)
        {


            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);
            try
            {

                string filePath = Path.Combine(outputPath, fileName);

                using (MemoryStream combinedStream = new MemoryStream())
                {
                    foreach (var chunk in fileSlices)
                    {
                        chunk.CopyTo(combinedStream);
                    }

                    // ذخیره فایل

                    File.WriteAllBytes(filePath, combinedStream.ToArray());
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            // ترکیب قطعات فایل

        }

        public async Task CreateFileImagesInTempFolder(IList<string> filePaths, Guid orderId)
        {
            if (filePaths != null && filePaths.Count > 0)
            {
                for (int i = 0; i < filePaths.Count; i++)
                {
                    var filePath = filePaths[i];
                    if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                    {
                        // مسیر فایل اصلی
                        var fileDirectory = Path.GetDirectoryName(filePath);
                        var filePureName = Path.GetFileName(filePath);

                        var imageFileAddress = Path.Combine(fileDirectory, filePureName);

                        if (File.Exists(imageFileAddress))
                        {
                            // دریافت نام پوشه منبع
                            string sourceDirectory = Path.GetFileName(fileDirectory.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
                            // مسیر جدید در wwwroot
                            var targetFilePath = Path.Combine(GetWebRootPath(), "TempFolder", orderId.ToString(), sourceDirectory, filePureName);

                            // بررسی وجود فایل در مقصد
                            if (!File.Exists(targetFilePath))
                            {
                                var targetDirectory = Path.GetDirectoryName(targetFilePath);
                                if (!Directory.Exists(targetDirectory))
                                {
                                    Directory.CreateDirectory(targetDirectory);
                                }

                                // کپی فایل
                                using (var sourceStream = File.OpenRead(imageFileAddress))
                                using (var targetStream = File.Create(targetFilePath))
                                {
                                    await sourceStream.CopyToAsync(targetStream);
                                }
                            }

                            // ایجاد URL جدید برای فایل کپی شده
                            var newFileUrl = $"{GetHostUrl()}/TempFolder/{orderId.ToString()}/{sourceDirectory}/{filePureName}";
                            filePaths[i] = newFileUrl;
                        }
                    }
                }
            }
        }



        public async Task<string> SaveNamedFile(IFormFile file, string? containerName = null, string? fileName = null)
        {
            try
            {
                string extension = Path.GetExtension(file.FileName);

                if (string.IsNullOrEmpty(fileName))
                    fileName = $"{Guid.NewGuid()}{extension}";
                else
                    fileName = $"{fileName}{extension}";

                string folder;
                if (string.IsNullOrEmpty(containerName))
                    folder = _webHostEnvironment.WebRootPath;
                else
                    folder = Path.Combine(_webHostEnvironment.WebRootPath, containerName);

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string route = Path.Combine(folder, fileName);
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var content = ms.ToArray();
                    await File.WriteAllBytesAsync(route, content);
                }

                return fileName;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> SaveFullPathFile(IFormFile file, string fullPath, string? fileName = null)
        {
            try
            {
                string extension = Path.GetExtension(file.FileName);

                if (string.IsNullOrEmpty(fileName))
                    fileName = file.FileName;
                else
                    fileName = $"{fileName}{extension}";


                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);

                string route = Path.Combine(fullPath, fileName);
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var content = ms.ToArray();
                    await File.WriteAllBytesAsync(route, content);
                }

                return route;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}