using INT1448.Application.Infrastructure.Core;
using INT1448.Application.Infrastructure.DTOs;
using INT1448.Application.IServices;
using INT1448.Core.Models;
using INT1448.Shared.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace INT1448.WebApi.Controllers
{
    [RoutePrefix("api/bookauthors")]
    public class BookAuthorController : ApiControllerBase
    {
        private IBookAuthorService _bookAuthorService;

        public BookAuthorController(IBookAuthorService bookAuthorService)
        {
            this._bookAuthorService = bookAuthorService;
        }

        [Route("getallbyauthorid/{id:int}")]
        [HttpGet]
        [ValidateModelAttribute]
        [IDFilterAttribute]
        public async Task<HttpResponseMessage> GetAllByAuthorId(int id)
        {
            HttpRequestMessage requestMessage = this.Request;
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<BookAuthorDTO> bookAuthorsDto = await _bookAuthorService.GetByAuthorId(id);
                response = requestMessage.CreateResponse(HttpStatusCode.OK, bookAuthorsDto, JsonMediaTypeFormatter.DefaultMediaType);

                return response;
            };

            return await CreateHttpResponseAsync(requestMessage, HandleRequest);
        }

        [Route("getallbybookid/{id:int}")]
        [HttpGet]
        [ValidateModelAttribute]
        [IDFilterAttribute]
        public async Task<HttpResponseMessage> GetAllByBookId(int id)
        {
            HttpRequestMessage requestMessage = this.Request;
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<BookAuthorDTO> bookAuthorsDto = await _bookAuthorService.GetByAuthorId(id);
                response = requestMessage.CreateResponse(HttpStatusCode.OK, bookAuthorsDto, JsonMediaTypeFormatter.DefaultMediaType);

                return response;
            };

            return await CreateHttpResponseAsync(requestMessage, HandleRequest);
        }

        [Route("update")]
        [HttpPut]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> Update(BookAuthorDTO bookAuthorDto)
        {
            HttpRequestMessage request = this.Request;
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                var dbBookAuhtor = await _bookAuthorService.GetById(bookAuthorDto.BookID, bookAuthorDto.AuthorID);

                await _bookAuthorService.Update(bookAuthorDto);
                await _bookAuthorService.SaveToDb();

                response = request.CreateResponse(HttpStatusCode.OK, dbBookAuhtor);

                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("create")]
        [HttpPost]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> Create(BookAuthorDTO bookAuthorDto)
        {
            HttpRequestMessage request = this.Request;
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                BookAuthorDTO bookAuthorAdded = await _bookAuthorService.Add(bookAuthorDto);
                await _bookAuthorService.SaveToDb();
                response = request.CreateResponse(HttpStatusCode.OK, bookAuthorAdded);
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("delete/{bookId:int}/{authorId:int}")]
        [HttpDelete]
        [ValidateModelAttribute]
        [IDFilterAttribute]
        public async Task<HttpResponseMessage> Delete(int bookId, int authorId)
        {
            HttpRequestMessage request = this.Request;
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                BookAuthorDTO bookAuthorDeleted = await _bookAuthorService.Delete(bookId, authorId);

                await _bookAuthorService.SaveToDb();
                response = request.CreateResponse(HttpStatusCode.OK, bookAuthorDeleted);

                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }
    }
}