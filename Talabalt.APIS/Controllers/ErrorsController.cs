using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabalt.APIS.Errors;

namespace Talabalt.APIS.Controllers
{
    [Route("Error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [HttpGet()]


        public ActionResult Error(int code)
        {
            switch (code)
            {
                case 404:
                   return NotFound(new ApiResponse(code)); break;

                case 401:
                    return Unauthorized(new ApiResponse(code)); break;

                    default: return StatusCode(code); break;
            }
        }
    }
}
