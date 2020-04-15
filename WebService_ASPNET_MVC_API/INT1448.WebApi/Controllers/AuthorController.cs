using INT1448.Application.Infrastructure.Core;
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
    [RoutePrefix("api/authors")]
    public class AuthorController : ApiControllerBase
    {
        private IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            this._authorService = authorService;
        }

        [Route("getall")]
        [HttpGet]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> GetAll(HttpRequestMessage requestMessage = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<Author> authors = await _authorService.GetAll();
                response = requestMessage.CreateResponse(HttpStatusCode.OK, authors, JsonMediaTypeFormatter.DefaultMediaType);

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
                Author Author = null;

                Author = await _authorService.GetById(id);

                if (Author == null)
                {
                    var message = new NotificationResponse("true", "Not found.");
                    response = request.CreateResponse(HttpStatusCode.NotFound, message, JsonMediaTypeFormatter.DefaultMediaType);
                    return response;
                }
                response = request.CreateResponse(HttpStatusCode.OK, Author);
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
                    IEnumerable<Author> authors = await _authorService.GetAll();
                    response = request.CreateResponse(HttpStatusCode.OK, authors);
                }
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("update")]
        [HttpPut]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> Update(Author Author, HttpRequestMessage request = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                var dbBookCategory = await _authorService.GetById(Author.ID);

                await _authorService.Update(Author);
                await _authorService.SaveToDb();

                response = request.CreateResponse(HttpStatusCode.OK, dbBookCategory);

                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("create")]
        [HttpPost]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> Create(Author Author, HttpRequestMessage request = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                Author bookCategoryAdded = await _authorService.Add(Author);
                await _authorService.SaveToDb();
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

                Author bookCategoryDeleted = await _authorService.Delete(id);

                await _authorService.SaveToDb();
                response = request.CreateResponse(HttpStatusCode.OK, bookCategoryDeleted);

                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }
    }
}