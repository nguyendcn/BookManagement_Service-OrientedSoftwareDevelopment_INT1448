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
    [RoutePrefix("api/publishers")]
    public class PublisherController : ApiControllerBase
    {
        private IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [Route("getall")]
        [HttpGet]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> GetAll(HttpRequestMessage requestMessage = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<PublisherDTO> publishers = await _publisherService.GetAll();
                response = requestMessage.CreateResponse(HttpStatusCode.OK, publishers, JsonMediaTypeFormatter.DefaultMediaType);

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
                PublisherDTO publisherDto = null;

                publisherDto = await _publisherService.GetById(id);

                if (publisherDto == null)
                {
                    var message = new NotificationResponse("true", "Not found.");
                    response = request.CreateResponse(HttpStatusCode.NotFound, message, JsonMediaTypeFormatter.DefaultMediaType);
                    return response;
                }
                response = request.CreateResponse(HttpStatusCode.OK, publisherDto);
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
                    IEnumerable<PublisherDTO> publishers = await _publisherService.GetAll();
                    response = request.CreateResponse(HttpStatusCode.OK, publishers);
                }
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("update")]
        [HttpPut]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> Update(HttpRequestMessage request, PublisherDTO publisherDto)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                var dbPublisher = await _publisherService.GetById(publisherDto.ID);

                await _publisherService.Update(publisherDto);
                await _publisherService.SaveToDb();

                response = request.CreateResponse(HttpStatusCode.OK, dbPublisher);

                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("create")]
        [HttpPost]
        [ValidateModelAttribute]
        public async Task<HttpResponseMessage> Create(PublisherDTO publisherDto, HttpRequestMessage request = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () =>
            {
                HttpResponseMessage response = null;

                PublisherDTO publisherAdded = await _publisherService.Add(publisherDto);
                await _publisherService.SaveToDb();
                response = request.CreateResponse(HttpStatusCode.OK, publisherAdded);
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

                PublisherDTO publisherDeleted = await _publisherService.Delete(id);

                await _publisherService.SaveToDb();
                response = request.CreateResponse(HttpStatusCode.OK, publisherDeleted);

                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }
    }
}