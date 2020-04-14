using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.Storage
{
    public class FileStorageService : IStorageService
    {
        private readonly string _userContentFolder;
        private const string BOOK_IMAGES_FOLDER_NAME = "book-images";

        public FileStorageService()
        {
            string rootPath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/user-content");
            _userContentFolder = Path.Combine(rootPath, BOOK_IMAGES_FOLDER_NAME);
                
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{BOOK_IMAGES_FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            using (var output = new FileStream(filePath, FileMode.Create))
            {
                await mediaBinaryStream.CopyToAsync(output);
            };
        }
    }
}
