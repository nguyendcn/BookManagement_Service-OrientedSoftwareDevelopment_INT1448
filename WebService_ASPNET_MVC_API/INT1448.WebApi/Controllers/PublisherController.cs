using INT1448.Application.Infrastructure.Core;
using INT1448.Application.IServices;
using INT1448.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace INT1448.WebApi.Controllers
{
    [RoutePrefix("api/publishers/")]
    public class PublisherController : ApiControllerBase
    {
        private IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [Route("getall")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAll(HttpRequestMessage requestMessage=null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () => {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    IEnumerable<Publisher> publishers = await _publisherService.GetAll();
                    response = requestMessage.CreateResponse(HttpStatusCode.OK, publishers);
                }
                return response;
            };

            return await CreateHttpResponseAsync(requestMessage, HandleRequest);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetById(int id, HttpRequestMessage request = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () => {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    Publisher publishers = await _publisherService.GetById(id);
                    response = request.CreateResponse(HttpStatusCode.OK, publishers);
                }
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("getallpublisherbook/{id:int}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllPublisherBook(int id, HttpRequestMessage request = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () => {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    IEnumerable<Publisher> publishers = await _publisherService.GetAll();
                    response = request.CreateResponse(HttpStatusCode.OK, publishers);
                }
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("update")]
        [HttpPut]
        public async Task<HttpResponseMessage> Update(HttpRequestMessage request, Publisher publisher)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () => {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbPublisher = await _publisherService.GetById(publisher.ID);

                    await _publisherService.Update(publisher);
                    await _publisherService.SaveToDb();

                    response = request.CreateResponse(HttpStatusCode.OK, dbPublisher);
                }
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("create")]
        [HttpPost]
        public async Task<HttpResponseMessage> Create(Publisher publisher, HttpRequestMessage request = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () => {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    Publisher publisherAdded = await _publisherService.Add(publisher);
                    await _publisherService.SaveToDb();
                    response = request.CreateResponse(HttpStatusCode.OK, publisherAdded);
                }
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(int id, HttpRequestMessage request = null)
        {
            Func<Task<HttpResponseMessage>> HandleRequest = async () => {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    Publisher publisherDeleted = await _publisherService.Delete(id);
                    await _publisherService.SaveToDb();
                    response = request.CreateResponse(HttpStatusCode.OK, publisherDeleted);
                }
                return response;
            };

            return await CreateHttpResponseAsync(request, HandleRequest);
        }
    }
}
