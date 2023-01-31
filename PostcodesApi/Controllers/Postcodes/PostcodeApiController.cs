using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PostcodesApi.App.Handlers.Postcodes.Queries;

namespace Web.Controllers.Postcodes
{
    [Route("api/postcodes")]
    [ApiController]
    public class PostcodeApiController : MediatorBaseController
    {
        private readonly IMemoryCache _memoryCache;

        public PostcodeApiController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        [Route("GetPostcode/{postcode}")]
        [ProducesResponseType(typeof(GetPostcodeQueryVm), 200)]
        public async Task<ActionResult<GetPostcodeQueryVm>> GetPostcode(string postcode)
        { 
            if (!_memoryCache.TryGetValue(postcode, out GetPostcodeQueryVm? vm))
            {
                vm = await Mediator.Send(new GetPostcodeQuery(postcode))!;
            }

            return Ok(vm);
        }
    }
}
