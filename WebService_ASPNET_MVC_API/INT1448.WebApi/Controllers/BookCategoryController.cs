using INT1448.Application.Infrastructure.Core;
using INT1448.Application.Infrastructure.DTOs;
using INT1448.Application.IServices;
using INT1448.Core.Models;
using INT1448.Shared.CommonType;
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
    [RoutePrefix("api/bookcategories")]
    public class BookCategoryController : ApiControllerBase
    {
        private IBookCategoryService _bookCategoryService;

        public BookCategoryController(IBookCategoryService bookCategoryService)
        {
            this._bookCategoryService = bookCategoryService;
        }

        [Route("getall")]
        [HttpGet]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> GetAll()
        {
            HttpRequestMessage requestMessage = this.Request;
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<BookCategoryDTO> bookCategoriesDto = await _bookCategoryService.GetAll();
                response = requestMessage.CreateResponse(HttpStatusCode.OK, bookCategoriesDto, JsonMediaTypeFormatter.DefaultMediaType);

                return response;
            };

            return await CreateHttpResponseAsync(requestMessage, HandleRequest);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        [ValidateModelAttribute]
        [IDFilterAttribute]
        public async Task<HttpResponseMessage> GetById(int id)
        {
            HttpRequestMessage request = this.Request;
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;
                BookCategoryDTO bookCategorieDto = null;

                bookCategorieDto = await _bookCategoryService.GetById(id);

                if (bookCategorieDto == null)
                {
                    var message = new NotificationResponse("true", "Not found.");
                    response = request.CreateResponse(HttpStatusCode.NotFound, message, JsonMediaTypeFormatter.DefaultMediaType);
                    return response;
                }
                response = request.CreateResponse(HttpStatusCode.OK, bookCategorieDto);
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("getallpublisherbook/{id:int}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllPublisherBook(int id)
        {
            HttpRequestMessage request = this.Request;
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    IEnumerable<BookCategoryDTO> bookCategoriesDto = await _bookCategoryService.GetAll();
                    response = request.CreateResponse(HttpStatusCode.OK, bookCategoriesDto);
                }
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("update")]
        [HttpPut]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> Update(BookCategoryDTO bookCategoryDto)
        {
            HttpRequestMessage request = this.Request;
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                var dbBookCategory = await _bookCategoryService.GetById(bookCategoryDto.ID);

                await _bookCategoryService.Update(bookCategoryDto);
                await _bookCategoryService.SaveToDb();

                response = request.CreateResponse(HttpStatusCode.OK, new NotificationResponse("true", "Update Category successed."));

                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("create")]
        [HttpPost]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> Create(BookCategoryDTO bookCategoryDto)
        {
            HttpRequestMessage request = this.Request;
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                BookCategoryDTO bookCategoryDtoAdded = await _bookCategoryService.Add(bookCategoryDto);
                await _bookCategoryService.SaveToDb();

                response = request.CreateResponse(HttpStatusCode.OK, new NotificationResponse("true", "Create category successed."));
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

                BookCategoryDTO bookCategoryDtoDeleted = await _bookCategoryService.Delete(id);

                await _bookCategoryService.SaveToDb();

                response = request.CreateResponse(HttpStatusCode.OK, new NotificationResponse("true", "Delete category successed."));

                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }
    }
}