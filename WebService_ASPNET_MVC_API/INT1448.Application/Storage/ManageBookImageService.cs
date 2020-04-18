using INT1448.Application.Infrastructure.DTOs;
using INT1448.Application.IServices;
using INT1448.Shared.UploadDocs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace INT1448.Application.Storage
{
    public class ManageBookImageService : IManageBookImageService
    {
        private IStorageService _storageService;
        private IBookImageManagerService _bookImageManagerService;

        public ManageBookImageService(IStorageService storageService, IBookImageManagerService bookImageManagerService)
        {
            _storageService = storageService;
            _bookImageManagerService = bookImageManagerService;
        }

        public async Task SaveImage(IList<HttpContent> files, int bookId)
        {
            Func<Task> Save = async () =>
            {
                foreach (HttpContent file in files)
                {
                    var fileInfo = file.Headers.ContentDisposition;
                    var postedFile = await file.ReadAsStreamAsync();

                    if (fileInfo != null && file.Headers.ContentLength > 0)
                    {
                        
                        var ext = fileInfo.FileName.Replace("\"", "").Substring(fileInfo.FileName.LastIndexOf('.') - 1);
                        var extension = ext.ToLower();

                        string myImageFileName = Guid.NewGuid() + extension;
                        //  where you want to attach your imageurl

                        //if needed write the code to update the table
                        BookImageDTO bookImageDTO = new BookImageDTO()
                        {
                            BookId = bookId,
                            DateCreated = DateTime.Now,
                            Caption = "sorry my mistake",
                            FileSize = file.Headers.ContentLength ?? 0,
                            IsDefault = true,
                            SortOrder = 1,
                            ImagePath = $"{_storageService.GetFileUrl(myImageFileName)}"
                        };
                        await _bookImageManagerService.Add(bookImageDTO);
                        await _bookImageManagerService.SaveToDb();

                        await _storageService.SaveFileAsync(postedFile, myImageFileName);
                    }
                }
            };

            await Task.Run(Save);
        }

        public async Task DeleteByBookId(int bookId)
        {
            IEnumerable<BookImageDTO> bookImageDTOs = await _bookImageManagerService.GetAllByBookId(bookId);

            foreach(BookImageDTO book in bookImageDTOs)
            {
               await _storageService.DeleteFileAsync(book.ImagePath.Substring(book.ImagePath.LastIndexOf("/") + 1));
            }
        }
    }
}
