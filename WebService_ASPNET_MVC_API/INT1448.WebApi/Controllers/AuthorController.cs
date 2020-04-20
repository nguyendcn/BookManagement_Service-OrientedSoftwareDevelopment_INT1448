using AutoMapper;
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
    [RoutePrefix("api/authors")]
    public class AuthorController : ApiControllerBase
    {
        private IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            this._authorService = authorService;
            this._mapper = mapper;
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

                IEnumerable<AuthorDTO> authorDTOs = await _authorService.GetAll();

                response = requestMessage.CreateResponse(HttpStatusCode.OK, authorDTOs, JsonMediaTypeFormatter.DefaultMediaType);

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
                AuthorDTO authorDTO = null;

                authorDTO = await _authorService.GetById(id);

                if (authorDTO == null)
                {
                    var message = new NotificationResponse("true", "Not found.");
                    response = request.CreateResponse(HttpStatusCode.NotFound, message, JsonMediaTypeFormatter.DefaultMediaType);
                    return response;
                }
                response = request.CreateResponse(HttpStatusCode.OK, authorDTO);
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
                    IEnumerable<AuthorDTO> authorDTOs = await _authorService.GetAll();
                    response = request.CreateResponse(HttpStatusCode.OK, authorDTOs);
                }
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("update")]
        [HttpPut]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> Update(AuthorDTO authorDto)
        {
            HttpRequestMessage request = this.Request;
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                var dbAuthor = await _authorService.GetById(authorDto.ID);

                await _authorService.Update(authorDto);
                await _authorService.SaveToDb();

                response = request.CreateResponse(HttpStatusCode.OK, dbAuthor);

                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("create")]
        [HttpPost]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> Create(AuthorDTO authorDto)
        {
            HttpRequestMessage request = this.Request;
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                AuthorDTO authorAdded = await _authorService.Add(authorDto);
                await _authorService.SaveToDb();
                response = request.CreateResponse(HttpStatusCode.OK, authorAdded);
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

                AuthorDTO authorDeleted = await _authorService.Delete(id);

                await _authorService.SaveToDb();
                response = request.CreateResponse(HttpStatusCode.OK, authorDeleted);

                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }
    }
}