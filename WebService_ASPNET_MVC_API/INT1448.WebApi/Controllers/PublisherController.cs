using INT1448.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;


namespace INT1448.WebApi.Controllers
{
    [System.Web.Http.Route("api/publisher")]
    public class PublisherController : ApiController
    {
        private IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [System.Web.Mvc.HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var publishers = _publisherService.GetAll();
            if (publishers == null)
            {
                return NotFound();
            }
            return Ok(publishers);
        }
    }
}
