using MassAddress.Logic.Services;
using MassAddress.Models.Api;
using MassAddress.Models.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;

namespace MassAddress.Controllers
{
    [Route("api/[controller]/findstates")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IOptions<GoogleConfigModel> config;
        private readonly IHttpClientFactory clientFactory;

        public AddressController(IOptions<GoogleConfigModel> config, IHttpClientFactory clientFactory)
        {
            this.config = config;
            this.clientFactory = clientFactory;
        }

        // GET api/address/findstates
        [HttpGet]
        public ActionResult<IEnumerable<AddressDto>> Get(string input)
        {
            return new AddressService(this.config, this.clientFactory).FindStates(input);
        }
    }
}
