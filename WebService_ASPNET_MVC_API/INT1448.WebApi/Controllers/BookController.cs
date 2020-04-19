using AutoMapper;
using INT1448.Application.Infrastructure.Core;
using INT1448.Application.Infrastructure.DTOs;
using INT1448.Application.Infrastructure.ViewModels;
using INT1448.Application.IServices;
using INT1448.Shared.CommonType;
using INT1448.Shared.Filters;
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
using INT1448.Application.Infrastructure.RequestTypes;

namespace INT1448.WebApi.Controllers
{
    [RoutePrefix("api/books")]
    public class BookController : ApiControllerBase
    {
        private IBookService _bookService;
        private IBookImageManagerService _bookImageManagerService;
        private IMapper _mapper;

        public BookController(IBookService bookService, 
            IBookImageManagerService bookImageManagerService,
            IMapper mapper)
        {
            this._bookService = bookService;
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
        public async Task<HttpResponseMessage> Update(BookUpdateRequest book)
        {
            HttpRequestMessage request = this.Request;
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                BookDTO bookToUpdate = _mapper.Map<BookDTO>(book);

                await _bookService.Update(bookToUpdate);
                await _bookService.SaveToDb();

                IEnumerable<BookImageDTO> bookImages = await _bookImageManagerService.GetAllByBookId(book.Id);

                IEnumerable<string> imagePaths = book.BookImages;

                IEnumerable<string> dbImagePaths = (bookImages).Select(x=>x.ImagePath);

                IEnumerable<string> join = from src in imagePaths
                                               join db in dbImagePaths
                                               on src equals db
                                               select src;
                int joinCount = join.Count();
                int srcCount = imagePaths.Count();
                int dbCount = dbImagePaths.Count();

                if(joinCount == srcCount && joinCount == dbCount) //not changed
                {
                    
                }
                else if(joinCount < srcCount && joinCount == dbCount) //insert
                {
                    IEnumerable<string> diffirents = imagePaths.Except(dbImagePaths);
                    foreach (string filePath in diffirents)
                    {
                        await _bookImageManagerService.Add(new BookImageDTO() { BookId = book.Id, ImagePath = filePath});
                    }
                }
                else if(joinCount  == srcCount && joinCount < dbCount) //delete
                {
                    IEnumerable<string> diffirents = dbImagePaths.Except(imagePaths);
                    foreach (string filePath in diffirents)
                    {
                        BookImageDTO bookImage = bookImages.Where(x => x.ImagePath == filePath).Single();
                        await _bookImageManagerService.Delete(bookImage.Id);
                    }
                    await _bookImageManagerService.SaveToDb();
                }
                else if(joinCount < srcCount && joinCount < dbCount) // insert and delete
                {
                    IEnumerable<string> inserted = imagePaths.Except(dbImagePaths);
                    IEnumerable<string> deleted = dbImagePaths.Except(imagePaths);

                    foreach (string filePath in inserted)
                    {
                        await _bookImageManagerService.Add(new BookImageDTO() { BookId = book.Id, ImagePath = filePath });
                    }

                    foreach (string filePath in deleted)
                    {
                        BookImageDTO bookImage = bookImages.Where(x => x.ImagePath == filePath).Single();
                        await _bookImageManagerService.Delete(bookImage.Id);
                    }
                    await _bookImageManagerService.SaveToDb();
                }
                else
                {

                }

                response = this.Request.CreateResponse(HttpStatusCode.OK, new NotificationResponse("tre", "Update book successed."));
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("create")]
        [HttpPost]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> Create(BookCreateRequest bookCreate)
        {
            HttpRequestMessage request = this.Request;

            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                BookDTO bookToAdd = _mapper.Map<BookDTO>(bookCreate);

                BookDTO bookAdded = await _bookService.Add(bookToAdd);

                IEnumerable<string> imagePaths = bookCreate.BookImages;

                foreach(string imagePath in imagePaths)
                {
                    await _bookImageManagerService.Add(new BookImageDTO { BookId = bookAdded.ID, ImagePath = imagePath });
                }

                response = request.CreateResponse(HttpStatusCode.OK, new NotificationResponse("true", "Book added to database."));
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

                await _bookImageManagerService.DeleteAllByBookId(id);

                await _bookImageManagerService.SaveToDb();
                await _bookService.SaveToDb();

                response = request.CreateResponse(HttpStatusCode.OK, bookDtoDeleted);

                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

    }
}