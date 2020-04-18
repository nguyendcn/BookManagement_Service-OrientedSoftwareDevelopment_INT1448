using AutoMapper;
using INT1448.Application.Infrastructure.Core;
using INT1448.Application.Infrastructure.DTOs;
using INT1448.Application.Infrastructure.Requests;
using INT1448.Application.Infrastructure.ViewModels;
using INT1448.Application.IServices;
using INT1448.Application.Storage;
using INT1448.Shared.CommonType;
using INT1448.Shared.Filters;
using INT1448.Shared.UploadDocs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Linq;

namespace INT1448.WebApi.Controllers
{
    [RoutePrefix("api/books")]
    public class BookController : ApiControllerBase
    {
        private IBookService _bookService;
        private IManageBookImageService _manageBookImageService;
        private IBookImageManagerService _bookImageManagerService;
        private IMapper _mapper;

        public BookController(IBookService bookService, 
            IManageBookImageService manageBookImageService,
            IBookImageManagerService bookImageManagerService,
            IMapper mapper)
        {
            this._bookService = bookService;
            this._manageBookImageService = manageBookImageService;
            this._bookImageManagerService = bookImageManagerService;
            this._mapper = mapper;
        }

        [Route("getall")]
        [HttpGet]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> GetAll(HttpRequestMessage requestMessage = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<BookDTO> bookDtos = await _bookService.GetAll();

                response = requestMessage.CreateResponse(HttpStatusCode.OK, bookDtos, JsonMediaTypeFormatter.DefaultMediaType);

                return response;
            };

            return await CreateHttpResponseAsync(requestMessage, HandleRequest);
        }

        [Route("getalltoview")]
        [HttpGet]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> GetAllToView()
        {
            HttpRequestMessage requestMessage = this.Request;
            
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<BookViewModel> bookDtos = await _bookService.GetAllToView();

                response = requestMessage.CreateResponse(HttpStatusCode.OK, bookDtos, JsonMediaTypeFormatter.DefaultMediaType);

                return response;
            };

            return await CreateHttpResponseAsync(requestMessage, HandleRequest);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        [ValidateModelAttribute]
        [IDFilterAttribute]
        public async Task<HttpResponseMessage> GetById(int id, HttpRequestMessage request = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;
                BookDTO bookDto = null;

                bookDto = await _bookService.GetById(id);

                if (bookDto == null)
                {
                    var message = new NotificationResponse("true", "Not found.");
                    response = request.CreateResponse(HttpStatusCode.NotFound, message, JsonMediaTypeFormatter.DefaultMediaType);
                    return response;
                }
                response = request.CreateResponse(HttpStatusCode.OK, bookDto);
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("getbyidtoview/{id:int}")]
        [HttpGet]
        [ValidateModelAttribute]
        [IDFilterAttribute]
        public async Task<HttpResponseMessage> GetByIdToView(int id)
        {
            HttpRequestMessage request = this.Request;

            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;
                BookViewModel bookDto = null;

                bookDto = await _bookService.GetByIdToView(id);

                if (bookDto == null)
                {
                    var message = new NotificationResponse("true", "Not found.");
                    response = request.CreateResponse(HttpStatusCode.NotFound, message, JsonMediaTypeFormatter.DefaultMediaType);
                    return response;
                }
                response = request.CreateResponse(HttpStatusCode.OK, bookDto);
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("update")]
        [HttpPut]
        [ValidateModelAttribute]
        [ValidateIMMCAttribute]
        public async Task<HttpResponseMessage> Update()
        {
            HttpRequestMessage request = this.Request;
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                InMemoryMultipartFormDataStreamProvider provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
                //access form data  
                NameValueCollection formData = provider.FormData;
                var json = formData.GetValues(formData.AllKeys[0]);

                BookRequestUpdate bookRequest = new JavaScriptSerializer().Deserialize<BookRequestUpdate>(json[0]);
                BookDTO bookUpdate = _mapper.Map<BookRequestUpdate, BookDTO>(bookRequest);

                await _bookService.Update(bookUpdate);

                if (!bookRequest.ImageStatus.IsModified)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, bookUpdate);
                    return response;
                }
                else
                {
                    ImageStatus imageStatus = bookRequest.ImageStatus;

                    //access files  
                    IList<HttpContent> files = provider.Files;

                    switch (imageStatus.ModifyType)
                    {
                        case ModifyType.DELETED :
                            await HandlingUpdateFileIsDeleted(imageStatus.ImageIdModified, bookRequest.Id);
                            break;
                        case ModifyType.INSERTED:
                            await HandlingUpdateFileIsInsert(files, bookRequest.Id);
                            break;
                        case ModifyType.INSERTED_DELETED:
                            await HandlingUpdateFileIsInsertDelete(imageStatus.ImageIdModified, files, bookRequest.Id);
                            break;
                        default:
                            break;
                    }
                    response = request.CreateResponse(HttpStatusCode.OK, new string[] { "Success: true", "Message: Update book success!" });
                    return response;
                }  
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("create")]
        [HttpPost]
        [ValidateModelAttribute]
        [ValidateIMMCAttribute]
        public async Task<HttpResponseMessage> Create()
        {
            HttpRequestMessage request = this.Request;

            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
                //access form data  
                NameValueCollection formData = provider.FormData;
                var json = formData.GetValues(formData.AllKeys[0]);

                BookDTO book = new JavaScriptSerializer().Deserialize<BookDTO>(json[0]);
          
                BookDTO bookAdded = await _bookService.Add(book);

                //access files  
                IList<HttpContent> files = provider.Files;

                await _manageBookImageService.SaveImage(files, bookAdded.ID);


                response = request.CreateResponse(HttpStatusCode.OK, bookAdded);
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        [ValidateModelAttribute]
        [IDFilterAttribute]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            HttpRequestMessage request = this.Request;
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                BookDTO bookDtoDeleted = await _bookService.Delete(id);

                await _manageBookImageService.DeleteByBookId(id);

                await _bookImageManagerService.DeleteAllByBookId(id);

                await _bookImageManagerService.SaveToDb();
                await _bookService.SaveToDb();

                response = request.CreateResponse(HttpStatusCode.OK, bookDtoDeleted);

                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        #region Function support

        private async Task HandlingUpdateFileIsDeleted(IEnumerable<int> idDeleteds, int bookId)
        {
            Func<Task> Handling = async () =>
            {


                var bookImage = await _bookImageManagerService.GetAllByBookId(bookId);

                var bookImageToDelete = from src in bookImage
                                        join del in idDeleteds
                                        on src.Id equals del
                                        select src;

                //Delete file in explore
                await _manageBookImageService.DeleteMulti(
                    bookImageToDelete.Select(t => (
                    t.ImagePath.Substring(t.ImagePath.LastIndexOf("/") + 1))
                    )
                );

                //Delete in database
                foreach (var b in bookImageToDelete)
                {
                    await _bookImageManagerService.Delete(b.Id);
                }

                await _bookImageManagerService.SaveToDb();
            };

            await Task.Run(Handling);
        }

        private async Task HandlingUpdateFileIsInsert(IList<HttpContent> files, int bookId)
        {
            await Task.Run(async () =>
            {
                await _manageBookImageService.SaveImage(files, bookId);
            });
        }

        private async Task HandlingUpdateFileIsInsertDelete(IEnumerable<int> idDeleteds, IList<HttpContent> files, int bookId)
        {
            await Task.Run(async () =>
            {
                await HandlingUpdateFileIsDeleted(idDeleteds, bookId);

                await HandlingUpdateFileIsInsert(files, bookId);

            });
        }

        #endregion
    }
}