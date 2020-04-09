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
    [RoutePrefix("apis/publisher")]
    public class PublisherController : ApiControllerBase
    {
        private IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [Route("getall")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAll(HttpRequestMessage requestMessage)
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
    }
}
