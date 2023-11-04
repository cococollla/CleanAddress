using AutoMapper;
using CleanAddress.Dadata.Client;
using CleanAddress.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanAddress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressContoller : ControllerBase
    {
        private readonly IDadataClient _client;
        private readonly IMapper _mapper;

        public AddressContoller(IMapper mapper, IDadataClient client)
        {
            _mapper = mapper;
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> GetCleanAddress([FromQuery] string address)
        {
            try
            {
                var result = _mapper.Map<Address>(await _client.GetStandardizedAddress(address));

                if (result.Result == null)
                {
                    return new JsonResult("Не получилось стандартизировать адрес");
                }

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult("Что-то пошло не так");
            }
        }
    }
}
