using INT1448.Application.Infrastructure.Core;
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
using System.Web.Mvc;

namespace INT1448.WebApi.Controllers
{
    [RoutePrefix("api/bookcategory")]
    public class BookCategoryController : ApiControllerBase
    {
        IBookCategoryService _bookCategoryService;

        public BookCategoryController(IBookCategoryService bookCategoryService)
        {
            this._bookCategoryService = bookCategoryService;
        }

        [Route("getall")]
        [HttpGet]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> GetAll(HttpRequestMessage requestMessage = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<BookCategory> bookCategories = await _bookCategoryService.GetAll();
                response = requestMessage.CreateResponse(HttpStatusCode.OK, bookCategories, JsonMediaTypeFormatter.DefaultMediaType);

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
                BookCategory bookCategory = null;

                bookCategory = await _bookCategoryService.GetById(id);

                if (bookCategory == null)
                {
                    var message = new NotificationResponse("true", "Not found.");
                    response = request.CreateResponse(HttpStatusCode.NotFound, message, JsonMediaTypeFormatter.DefaultMediaType);
                    return response;
                }
                response = request.CreateResponse(HttpStatusCode.OK, bookCategory);
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("getallpublisherbook/{id:int}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllPublisherBook(int id, HttpRequestMessage request = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    IEnumerable<BookCategory> bookCategories = await _bookCategoryService.GetAll();
                    response = request.CreateResponse(HttpStatusCode.OK, bookCategories);
                }
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("update")]
        [HttpPut]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> Update(BookCategory bookCategory, HttpRequestMessage request = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                var dbBookCategory = await _bookCategoryService.GetById(bookCategory.ID);

                await _bookCategoryService.Update(bookCategory);
                await _bookCategoryService.SaveToDb();

                response = request.CreateResponse(HttpStatusCode.OK, dbBookCategory);

                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("create")]
        [HttpPost]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> Create(BookCategory bookCategory, HttpRequestMessage request = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                BookCategory bookCategoryAdded = await _bookCategoryService.Add(bookCategory);
                await _bookCategoryService.SaveToDb();
                response = request.CreateResponse(HttpStatusCode.OK, bookCategoryAdded);
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        [ValidateModelAttribute]
        [IDFilterAttribute]
        public async Task<HttpResponseMessage> Delete(int id, HttpRequestMessage request = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                BookCategory bookCategoryDeleted = await _bookCategoryService.Delete(id);

                await _bookCategoryService.SaveToDb();
                response = request.CreateResponse(HttpStatusCode.OK, bookCategoryDeleted);

                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }
    }
}
