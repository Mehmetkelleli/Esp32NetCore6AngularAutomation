using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartBusinessApplication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IMapper _Mapper;
        public BaseController(IMapper Mapper)
        {
            _Mapper = Mapper;
        }
    }
}
