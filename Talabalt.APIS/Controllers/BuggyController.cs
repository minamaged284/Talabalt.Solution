using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabalt.APIS.Errors;
using Talabat.Core.Entities;
using Talabat.Repository.Data;

namespace Talabalt.APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreDbContext _dbocntext;

        public BuggyController(StoreDbContext dbocntext)
        {
           _dbocntext = dbocntext;
        }


        [HttpGet("notFound")]

        public ActionResult GetNotFound()
        {
            return NotFound(new ApiResponse(404));
        }


        [HttpGet("badRequest")]

        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }


        [HttpGet("nullReference")]
        public ActionResult GetNUll()
        {
            var product = _dbocntext.Products.Where(p => p.Id == 112121).ToString;
            return Ok(product);
        }


        [HttpGet("validation/{id}")]
        public ActionResult GetValidation(int id) {

            return Ok(_dbocntext.Products.Find(id));
        }

    }
}
