﻿using INT1448.Application.Infrastructure.Core;
using INT1448.Application.IServices;
using INT1448.Core.Models;
using INT1448.Shared.CommonType;
using INT1448.Shared.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
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
        IBookAuthorService _bookAuthorService;

        public BookAuthorController(IBookAuthorService bookAuthorService)
        {
            this._bookAuthorService = bookAuthorService;
        }

        [Route("getallbyauthorid/{id:int}")]
        [HttpGet]
        [ValidateModelAttribute]
        [IDFilterAttribute]
        public async Task<HttpResponseMessage> GetAllByAuthorId(int id, HttpRequestMessage requestMessage = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<BookAuthor> bookAuthors = await _bookAuthorService.GetByAuthorId(id);
                response = requestMessage.CreateResponse(HttpStatusCode.OK, bookAuthors, JsonMediaTypeFormatter.DefaultMediaType);

                return response;
            };

            return await CreateHttpResponseAsync(requestMessage, HandleRequest);
        }

        [Route("getallbybookid/{id:int}")]
        [HttpGet]
        [ValidateModelAttribute]
        [IDFilterAttribute]
        public async Task<HttpResponseMessage> GetAllByBookId(int id, HttpRequestMessage requestMessage = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<BookAuthor> bookAuthors = await _bookAuthorService.GetByAuthorId(id);
                response = requestMessage.CreateResponse(HttpStatusCode.OK, bookAuthors, JsonMediaTypeFormatter.DefaultMediaType);

                return response;
            };

            return await CreateHttpResponseAsync(requestMessage, HandleRequest);
        }

        [Route("update")]
        [HttpPut]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> Update(BookAuthor bookAuthor, HttpRequestMessage request = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                var dbBookAuhtor = await _bookAuthorService.GetById(bookAuthor.BookID, bookAuthor.AuthorID);

                await _bookAuthorService.Update(bookAuthor);
                await _bookAuthorService.SaveToDb();

                response = request.CreateResponse(HttpStatusCode.OK, dbBookAuhtor);

                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("create")]
        [HttpPost]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> Create(BookAuthor bookAuthor, HttpRequestMessage request = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                BookAuthor bookAuthorAdded = await _bookAuthorService.Add(bookAuthor);
                await _bookAuthorService.SaveToDb();
                response = request.CreateResponse(HttpStatusCode.OK, bookAuthorAdded);
                response = request.CreateResponse(HttpStatusCode.OK, bookAuthorAdded);
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("delete/{bookId:int}/{authorId:int}")]
        [HttpDelete]
        [ValidateModelAttribute]
        [IDFilterAttribute]
        public async Task<HttpResponseMessage> Delete(int bookId, int authorId, HttpRequestMessage request = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                BookAuthor bookAuthorDeleted = await _bookAuthorService.Delete(bookId, authorId);

                await _bookAuthorService.SaveToDb();
                response = request.CreateResponse(HttpStatusCode.OK, bookAuthorDeleted);

                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }
    }
}